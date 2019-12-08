using Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;

using static Entity.FinalcialInformationType;

namespace DataAccessLayer
{
    public class TransportFormRepository : Repository, ISave<TransportForm>, ISave<Ticket>, ISearch<TransportForm>, IUpdate, IMap<TransportForm>, IGetAllData<TransportForm>
    {
        static readonly string[] TRANSPORT_FORM_FIELDS = { "@transport_form_number", "@start_date", "@depature_time",
                                                           "@value_of_tickets", "@total_value", "@license_plate", "@route_code", 
                                                           "@dispatcher"};

        private readonly FinalcialInformationRepository _finalcialInformationRepository;
        private readonly TicketRepository _ticketRepository;

        public TransportFormRepository(DbConnection connection) : base(connection)
        {
            _finalcialInformationRepository = new FinalcialInformationRepository(connection);
            _ticketRepository = new TicketRepository(connection);
        }

        public bool Save(TransportForm data)
        {
            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = "INSERT INTO transport_forms(transport_form_number, start_date, depature_time, " +
                                      "value_of_tickets, total_value, license_plate, route_code, dispatcher) " +
                                      "VALUES(@transport_form_number, @start_date, @depature_time, " +
                                             "@value_of_tickets, @total_value, @license_plate, @route_code, @dispatcher)";

                MapCommandParameters(command, TRANSPORT_FORM_FIELDS,
                    new object[]
                    {
                        data.Number,
                        data.Date,
                        data.DepartureTime,
                        data.ValueOfTickets,
                        data.TotalValue,
                        data.Vehicle.LicensePlate,
                        data.Route.Code,
                        data.Dispatcher.ID
                    });

                command.ExecuteNonQuery();
                return true;
            }
        }

        public bool Save(Ticket data)
        {
            return _ticketRepository.Save(data);
        }

        public TransportForm Search(string primaryKey)
        {
            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = "SELECT tf.transport_form_number, tf.start_date, tf.depature_time, tf.value_of_tickets, " +
                                      "tf.total_value, tf.license_plate, tf.route_code, tf.dispatcher, fi.replacement_fund, " +
                                      "fi.social_contribution, fi.tire_service, fi.vehicle_fix_service, fi.non_contractual_secure_service, " +
                                      "fi.constact_insurance_service, fi.social_protection, fi.extraordinary_protection, " +
                                      "fi.administration, fi.others, fi.total FROM transport_forms tf " +
                                      "JOIN finalcial_information fi ON tf.transport_form_number = fi.transport_form_number " +
                                      "WHERE tf.transport_form_number = @transport_form_number";

                command.Parameters.Add(CreateDbParameter(command, "@transport_form_number", primaryKey));

                using (var dbDataReader = command.ExecuteReader())
                    return dbDataReader.Read() ? Map(dbDataReader) : null;
            }
        }

        public TransportForm Map(DbDataReader dbDataReader)
        {
            AdministrativeEmployeeRepository administrativeEmployeeRepository = new AdministrativeEmployeeRepository(dbConnection);
            AdministrativeEmployee dispatcher = administrativeEmployeeRepository.Search(dbDataReader.GetString(7), true);
            VehicleRepository vehicleRepository = new VehicleRepository(dbConnection);
            Vehicle vehicle = vehicleRepository.Search(dbDataReader.GetString(5));
            RouteRepository routeRepository = new RouteRepository(dbConnection);
            Route route = routeRepository.Search(dbDataReader.GetString(6));

            if (dispatcher is null || vehicle is null || route is null)
                return null;

            string transport_form_number = dbDataReader.GetString(0);
            DateTime startDate, depatureTime;

            startDate = dbDataReader.GetDateTime(1);
            depatureTime = dbDataReader.GetDateTime(2);

            TransportForm transportForm = new TransportForm(transport_form_number, route, vehicle, dispatcher, startDate, depatureTime);

            
            transportForm.AddFinalcialInformation(REPLACEMENT_FUND, dbDataReader.GetDecimal(8));
            transportForm.AddFinalcialInformation(SOCIAL_CONTRIBUTION, dbDataReader.GetDecimal(9));
            transportForm.AddFinalcialInformation(TIRE_SERVICE, dbDataReader.GetDecimal(10));
            transportForm.AddFinalcialInformation(VEHICLE_FIX_SERVICE, dbDataReader.GetDecimal(11));
            transportForm.AddFinalcialInformation(NON_CONTRACTUAL_SERCURE_SERVICE, dbDataReader.GetDecimal(12));
            transportForm.AddFinalcialInformation(CONSTACT_INSURANCE_SERVICE, dbDataReader.GetDecimal(13));
            transportForm.AddFinalcialInformation(SOCIAL_PROTECTION, dbDataReader.GetDecimal(14));
            transportForm.AddFinalcialInformation(EXTRAORDINARY_PROTECTION, dbDataReader.GetDecimal(15));
            transportForm.AddFinalcialInformation(ADMINISTRATION, dbDataReader.GetDecimal(16));
            transportForm.AddFinalcialInformation(OTHERS, dbDataReader.GetDecimal(17));
            transportForm.UpdateTotalValue();

            using (var ticketCommand = dbConnection.CreateCommand())
            {
                ticketCommand.CommandText = "SELECT seats, ticket_date, passenger FROM tickets WHERE transport_form_number = @transport_form_number";

                ticketCommand.Parameters.Add(CreateDbParameter(ticketCommand, "@transport_form_number", transportForm.Number));

                DbDataReader ticketDataReader = ticketCommand.ExecuteReader();

                while (ticketDataReader.Read())
                {
                    PersonRepository personRepository = new PersonRepository(dbConnection);

                    if (!(personRepository.Search(ticketDataReader.GetString(2)) is Passenger passenger))
                        continue;

                    transportForm.AddTicket(passenger, ticketDataReader.GetInt32(0), ticketDataReader.GetDateTime(1));
                }
            }

            return transportForm;
        }

        public bool Update(string primarykey, string columToModify, object newValue)
        {
            throw new System.NotImplementedException();
        }

        public IList<TransportForm> GetAllData()
        {
            IList<TransportForm> transportForms = new List<TransportForm>();

            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = "SELECT tf.transport_form_number, tf.start_date, tf.depature_time, tf.value_of_tickets, " +
                                      "tf.total_value, tf.license_plate, tf.route_code, tf.dispatcher, fi.replacement_fund, " +
                                      "fi.social_contribution, fi.tire_service, fi.vehicle_fix_service, fi.non_contractual_secure_service, " +
                                      "fi.constact_insurance_service, fi.social_protection, fi.extraordinary_protection, " +
                                      "fi.administration, fi.others, fi.total FROM transport_forms tf " +
                                      "JOIN finalcial_information fi ON tf.transport_form_number = fi.transport_form_number " +
                                      "WHERE tf.transport_form_number = @transport_form_number";

                using (var dbDataReader = command.ExecuteReader())
                {
                    while (dbDataReader.Read())
                        transportForms.Add(Map(dbDataReader));
                }
            }

            return transportForms;
        }

        class FinalcialInformationRepository : Repository, IUpdate
        {
            public FinalcialInformationRepository(DbConnection connection) : base(connection)
            {

            }

            public bool Update(string primarykey, string columToModify, object newValue)
            {
                throw new System.NotImplementedException();
            }
        }

        class TicketRepository : Repository, ISave<Ticket>
        {
            static readonly string[] TICKET_FIELDS = { "@ticket_number", "@seats", "@ticket_date", "@total",
                                                   "@transport_form_number", "@passenger"};
            public TicketRepository(DbConnection connection) : base(connection)
            {

            }

            public bool Save(Ticket data)
            {
                using (var command = dbConnection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO tickets(ticket_number, seats, ticket_date, total, transport_form_number, passenger) " +
                                          "VALUES(@ticket_number, @seats, @ticket_date, @total, @transport_form_number, @passenger)";

                    MapCommandParameters(command, TICKET_FIELDS,
                        new object[] {
                        data.Number,
                        data.Seats,
                        data.Date,
                        data.Total,
                        data.GetTransportFormCode(),
                        data.Passenger.ID
                        });

                    command.ExecuteNonQuery();
                    return true;
                }
            }
        }
    }
}

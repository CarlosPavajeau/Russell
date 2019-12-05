﻿using DataAccessLayer;
using Entity;

namespace BusinessLogicLayer
{
    public class TransportFormService : Service, ISave<TransportForm>, ISearch<TransportForm>, IUpdate
    {
        private readonly TransportFormRepository _transportFormRepository;

        public TransportFormService()
        {
            _transportFormRepository = new TransportFormRepository(dbConnection);
        }

        public bool Save(TransportForm data)
        {
            try
            {
                dbConnection.Open();
                return _transportFormRepository.Save(data);
            }
            catch (System.Exception)
            {
                return false;
            }
            finally
            {
                dbConnection?.Close();
            }
        }

        public TransportForm Search(string primaryKey)
        {
            try
            {
                dbConnection.Open();
                return _transportFormRepository.Search(primaryKey);
            }
            catch (System.Exception)
            {
                return null;
            }
            finally
            {
                dbConnection?.Close();
            }
        }

        public bool Update(string primarykey, string columToModify, object newValue)
        {
            try
            {
                dbConnection.Open();
                return _transportFormRepository.Update(primarykey, columToModify, newValue);
            }
            catch (System.Exception)
            {
                return false;
            }
            finally
            {
                dbConnection?.Close();
            }
        }
    }
}
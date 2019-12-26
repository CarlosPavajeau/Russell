namespace Common
{
    public enum ServerAnswer
    {
        SaveSuccessful,
        DataAlreadyRegistered,
        NotFoundData,
        SuccessfullyModified,
        FailedModification,
        SuccessfullyRemoved,
        DeletionFailed,
        IsTheFirstApplicationStart,
        IsNotTheFirstApplicationStart,
        InvalidCommand
    }
}

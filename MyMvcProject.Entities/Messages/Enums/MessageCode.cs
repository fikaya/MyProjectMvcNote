namespace MyMvcProject.Entities.Messages.Enums
{
    public enum MessageCode
    {
        UserNameAlreadyExists=101,
        EMailAlreadyExists=102,
        UserIsNotActive=201,
        UsernameOrPasswordWrong=202,
        CheckYourEMail = 301,
        UserAlreadyActive = 401,
        ActivateIDDoesNotExists=402,
        UserNotFound=501,
        RegisterSuccess=601,
        ActivationSuccess=602,
        ProfileCouldNotUpdated = 603,
        UserCouldNotRemove = 604,
        UserCouldNotFind = 605,
        UserCouldNotInserted = 606,
        RegisterUpdateSuccess = 607
    }
}

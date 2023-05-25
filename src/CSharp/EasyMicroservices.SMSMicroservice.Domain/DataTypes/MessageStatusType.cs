namespace EasyMicroservices.SMSMicroservice.DataTypes
{
    public enum MessageStatusType : byte
    {
        None = 0,
        Default = 1,
        All = 2,
        Other = 3,
        Unknown = 4,
        Nothing = 5,
        Queue = 6,
        Sending = 7,
        Sent = 8,
        Exception = 9,
        Canceled = 10
    }
}

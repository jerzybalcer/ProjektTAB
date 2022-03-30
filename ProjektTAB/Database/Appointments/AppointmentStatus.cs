namespace Database.Appointments
{
    public enum AppointmentStatus
    {
        Registered, // appointment will happen in the future
        Started,    // appointment started but not ended yet
        Finished,   // successfully ended
        Unattended, // patient didn't come
        Cancelled,  // patient asked to cancel it
        Failed      // patient came but doctor was unable to examine the patient
    }
}

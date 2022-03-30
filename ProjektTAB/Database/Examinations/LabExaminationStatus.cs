namespace Database.Examinations
{
    public enum LabExaminationStatus
    {
        Ordered,                // examination ordered by doctor, waits to be done
        SuccessfullyExecuted,   // done by assistant, waits for manager to check it
        CancelledByAssistant,   // there was some error during examination, couldn't finish
        AcceptedByManager,      // manager accepted it, finished successfully
        RejectedByManager,      // manager cancelled it
    }
}

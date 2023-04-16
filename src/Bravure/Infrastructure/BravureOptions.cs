namespace Bravure.Infrastructure
{
    public class BravureOptions
    {
        public bool UseDummyServices { get; set; }
        public string BaseUrl { get; set; }
        public string KDMSApiUrl { get; set; }
        public string FromName { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string FromAddress { get; set; }
        public bool SmtpSSLEnabled { get; set; } = true;
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public string AWSAccessStorageKey { get; set; }
        public string AWSSecretStorageKey { get; set; }
        public string AWSStorageBucketRequests { get; set; }
        public string AWSStorageBucketComplianceForms { get; set; }

        public bool BackgroundSalesScheduler { get; set; }
        public int BackgroundSalesSchedulerMinutesDelay { get; set; }
        public bool RunBackgroundPasswordService { get; set; }
        public int BackgroundPasswordServiceMinutesDelay { get; set; }
        public int RequestLastAccesssStoreSeconds { get; set; }
        public string HaveibeenpwnedApiKey { get; set; }
        public string HaveibeenpwnedApplicationName { get; set; }
        public int DaysUntilPasswordExpires { get; set; }
        public int DaysPriorToPasswordExpiryForEmailReminder { get; set; }
        public int NumPasswordsInHistory { get; set; }
        public string PdfGeneratorUrl { get; set; }
    }
}

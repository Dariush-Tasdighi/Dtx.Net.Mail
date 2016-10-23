namespace Dtx.Net.Mail
{
	[System.Serializable]
	public class MailSettings : IMailSettings
	{
		public MailSettings() : base()
		{
		}

		public bool SmtpClientEnableSsl { get; set; }

		public int SmtpClientTimeout { get; set; }

		public int SmtpClientPortNumber { get; set; }

		public string SmtpClientHostAddress { get; set; }

		public bool UseDefaultCredentials { get; set; }

		public string SmtpUsername { get; set; }

		public string SmtpPassword { get; set; }

		public string SenderEmailAddress { get; set; }

		public string SenderDisplayName { get; set; }

		public string EmailSubjectTemplate { get; set; }

		public string BccAddresses { get; set; }

		public string SupportEmailAddress { get; set; }

		public string SupportDisplayName { get; set; }
	}
}

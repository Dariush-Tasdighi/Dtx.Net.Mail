namespace Dtx.Net.Mail
{
	public interface IMailSettings
	{
		bool SmtpClientEnableSsl { get; set; }

		int SmtpClientTimeout { get; set; }

		int SmtpClientPortNumber { get; set; }

		string SmtpClientHostAddress { get; set; }

		bool UseDefaultCredentials { get; set; }

		string SmtpUsername { get; set; }

		string SmtpPassword { get; set; }

		string SenderEmailAddress { get; set; }

		string SenderDisplayName { get; set; }

		string EmailSubjectTemplate { get; set; }

		string BccAddresses { get; set; }

		string SupportEmailAddress { get; set; }

		string SupportDisplayName { get; set; }
	}
}

namespace Dtx.Net.Mail
{
	internal static class ApplicationSettings
	{
		static ApplicationSettings()
		{
		}

		// **************************************************
		private static bool? _smtpClientEnableSsl;

		internal static bool SmtpClientEnableSsl
		{
			get
			{
				if (_smtpClientEnableSsl.HasValue == false)
				{
					string strResult =
						Dtx.ApplicationSettings.GetValue("SmtpClientEnableSsl", defaultValue: "0");

					if (strResult == "1")
					{
						_smtpClientEnableSsl = true;
					}
					else
					{
						_smtpClientEnableSsl = false;
					}
				}

				return (_smtpClientEnableSsl.Value);
			}
		}
		// **************************************************

		// **************************************************
		private static bool? _useDefaultCredentials;

		internal static bool UseDefaultCredentials
		{
			get
			{
				if (_useDefaultCredentials.HasValue == false)
				{
					string strResult =
						Dtx.ApplicationSettings.GetValue("UseDefaultCredentials", defaultValue: "0");

					if (strResult == "1")
					{
						_useDefaultCredentials = true;
					}
					else
					{
						_useDefaultCredentials = false;
					}
				}

				return (_useDefaultCredentials.Value);
			}
		}
		// **************************************************

		// **************************************************
		private static int? _smtpClientTimeout;

		internal static int SmtpClientTimeout
		{
			get
			{
				if (_smtpClientTimeout.HasValue == false)
				{
					int intDefaultValue = 100000;

					string strResult =
						Dtx.ApplicationSettings.GetValue
						("SmtpClientTimeout", defaultValue: intDefaultValue.ToString());

					try
					{
						_smtpClientTimeout =
							System.Convert.ToInt32(strResult);
					}
					catch
					{
						_smtpClientTimeout = intDefaultValue;
					}
				}

				return (_smtpClientTimeout.Value);
			}
		}
		// **************************************************

		// **************************************************
		private static int? _smtpClientPortNumber;

		internal static int SmtpClientPortNumber
		{
			get
			{
				if (_smtpClientPortNumber.HasValue == false)
				{
					int intDefaultValue = 25;

					string strResult =
						Dtx.ApplicationSettings.GetValue
						("SmtpClientPortNumber", defaultValue: intDefaultValue.ToString());

					try
					{
						_smtpClientPortNumber =
							System.Convert.ToInt32(strResult);
					}
					catch
					{
						_smtpClientPortNumber = intDefaultValue;
					}
				}

				return (_smtpClientPortNumber.Value);
			}
		}
		// **************************************************

		// **************************************************
		private static string _smtpClientHostAddress;

		internal static string SmtpClientHostAddress
		{
			get
			{
				if (_smtpClientHostAddress == null)
				{
					_smtpClientHostAddress =
						Dtx.ApplicationSettings.GetValue("SmtpClientHostAddress");
				}

				return (_smtpClientHostAddress);
			}
		}
		// **************************************************

		// **************************************************
		private static string _smtpUsername;

		internal static string SmtpUsername
		{
			get
			{
				if (_smtpUsername == null)
				{
					_smtpUsername =
						Dtx.ApplicationSettings.GetValue("SmtpUsername");
				}

				return (_smtpUsername);
			}
		}
		// **************************************************

		// **************************************************
		private static string _smtpPassword;

		internal static string SmtpPassword
		{
			get
			{
				if (_smtpPassword == null)
				{
					_smtpPassword =
						Dtx.ApplicationSettings.GetValue("SmtpPassword");
				}

				return (_smtpPassword);
			}
		}
		// **************************************************

		// **************************************************
		private static string _senderEmailAddress;

		internal static string SenderEmailAddress
		{
			get
			{
				if (_senderEmailAddress == null)
				{
					_senderEmailAddress =
						Dtx.ApplicationSettings.GetValue("SenderEmailAddress");
				}

				return (_senderEmailAddress);
			}
		}
		// **************************************************

		// **************************************************
		private static string _senderDisplayName;

		internal static string SenderDisplayName
		{
			get
			{
				if (_senderDisplayName == null)
				{
					_senderDisplayName =
						Dtx.ApplicationSettings.GetValue("SenderDisplayName");
				}

				return (_senderDisplayName);
			}
		}
		// **************************************************

		// **************************************************
		private static string _emailSubjectTemplate;

		internal static string EmailSubjectTemplate
		{
			get
			{
				if (_emailSubjectTemplate == null)
				{
					_emailSubjectTemplate =
						Dtx.ApplicationSettings.GetValue("EmailSubjectTemplate");
				}

				return (_emailSubjectTemplate);
			}
		}
		// **************************************************

		// **************************************************
		private static string _bccAddresses;

		internal static string BccAddresses
		{
			get
			{
				if (_bccAddresses == null)
				{
					_bccAddresses =
						Dtx.ApplicationSettings.GetValue("BccAddresses");
				}

				return (_bccAddresses);
			}
		}
		// **************************************************

		// **************************************************
		private static string _supportEmailAddress;

		internal static string SupportEmailAddress
		{
			get
			{
				if (_supportEmailAddress == null)
				{
					_supportEmailAddress =
						Dtx.ApplicationSettings.GetValue("SupportEmailAddress");
				}

				return (_supportEmailAddress);
			}
		}
		// **************************************************

		// **************************************************
		private static string _supportDisplayName;

		internal static string SupportDisplayName
		{
			get
			{
				if (_supportDisplayName == null)
				{
					_supportDisplayName =
						Dtx.ApplicationSettings.GetValue("SupportDisplayName");
				}

				return (_supportDisplayName);
			}
		}
		// **************************************************
	}
}

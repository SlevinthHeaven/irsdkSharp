using irsdkSharp.Serialization.Models.Session.CameraInfo;
using irsdkSharp.Serialization.Models.Session.DriverInfo;
using irsdkSharp.Serialization.Models.Session.RadioInfo;
using irsdkSharp.Serialization.Models.Session.SessionInfo;
using irsdkSharp.Serialization.Models.Session.SplitTimeInfo;
using irsdkSharp.Serialization.Models.Session.WeekendInfo;
using System;
using System.IO;
using System.Text;
using YamlDotNet.Serialization;

namespace irsdkSharp.Serialization.Models.Session
{
	public class IRacingSessionModel
	{
		public static IRacingSessionModel Serialize( string yaml )
		{
			yaml = PreprocessYAML( yaml );
			var r = new StringReader( yaml );
			var deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();
			try
			{
				return deserializer.Deserialize<IRacingSessionModel>( r );
			}
			catch ( Exception ex )
			{
				Console.WriteLine( ex.Message );
				Console.WriteLine( ex.StackTrace );
				return null;
			}
		}

		public WeekendInfoModel WeekendInfo { get; set; }
		public SessionInfoModel SessionInfo { get; set; }
		// public QualifyResultsInfoModel QualifyResultsInfo { get; set; }
		public CameraInfoModel CameraInfo { get; set; }
		public RadioInfoModel RadioInfo { get; set; }
		public DriverInfoModel DriverInfo { get; set; }
		public SplitTimeInfoModel SplitTimeInfo { get; set; }

		public class KeyTracker
		{
			public string keyToFix = string.Empty;
			public int counter = 0;
			public bool ignoreUntilNextLine = false;
			public bool addFirstQuote = false;
			public bool addSecondQuote = false;
		}

		private static string PreprocessYAML( string yaml )
		{
			const int MaxNumDrivers = 63;
			const int MaxNumAdditionalCharactersPerFixedKey = 2;

			var keysToFix = new string[]
			{
				"AbbrevName:", "TeamName:", "UserName:", "Initials:", "DriverSetupName:"
			};

			var keyTrackers = new KeyTracker[ keysToFix.Length ];

			for ( var i = 0; i < keyTrackers.Length; i++ )
			{
				keyTrackers[ i ] = new KeyTracker()
				{
					keyToFix = keysToFix[ i ]
				};
			}

			var keyTrackersIgnoringUntilNextLine = 0;

			var stringBuilder = new StringBuilder( yaml.Length + keysToFix.Length * MaxNumAdditionalCharactersPerFixedKey * MaxNumDrivers );

			foreach ( var c in yaml )
			{
				if ( keyTrackersIgnoringUntilNextLine == keyTrackers.Length )
				{
					if ( c == '\n' )
					{
						keyTrackersIgnoringUntilNextLine = 0;

						foreach ( var keyTracker in keyTrackers )
						{
							keyTracker.counter = 0;
							keyTracker.ignoreUntilNextLine = false;
						}
					}
				}
				else
				{
					foreach ( var keyTracker in keyTrackers )
					{
						if ( keyTracker.ignoreUntilNextLine )
						{
							if ( c == '\n' )
							{
								keyTracker.counter = 0;
								keyTracker.ignoreUntilNextLine = false;

								keyTrackersIgnoringUntilNextLine--;
							}
						}
						else if ( keyTracker.addFirstQuote )
						{
							if ( c == '\n' )
							{
								keyTracker.counter = 0;
								keyTracker.addFirstQuote = false;
							}
							else if ( c != ' ' )
							{
								stringBuilder.Append( '\'' );

								keyTracker.addFirstQuote = false;
								keyTracker.addSecondQuote = true;
							}
						}
						else if ( keyTracker.addSecondQuote )
						{
							if ( c == '\n' )
							{
								stringBuilder.Append( '\'' );

								keyTracker.counter = 0;
								keyTracker.addSecondQuote = false;
							}
							else if ( c == '\'' )
							{
								stringBuilder.Append( '\'' );
							}
						}
						else
						{
							if ( c == keyTracker.keyToFix[ keyTracker.counter ] )
							{
								keyTracker.counter++;

								if ( keyTracker.counter == keyTracker.keyToFix.Length )
								{
									keyTracker.addFirstQuote = true;
								}
							}
							else if ( c != ' ' )
							{
								keyTracker.ignoreUntilNextLine = true;

								keyTrackersIgnoringUntilNextLine++;
							}
						}
					}
				}

				stringBuilder.Append( c );
			}

			return stringBuilder.ToString();
		}
	}
}

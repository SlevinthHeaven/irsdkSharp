using irsdkSharp.Serialization.Models.Session.CameraInfo;
using irsdkSharp.Serialization.Models.Session.DriverInfo;
using irsdkSharp.Serialization.Models.Session.QualifyResultsInfo;
using irsdkSharp.Serialization.Models.Session.RadioInfo;
using irsdkSharp.Serialization.Models.Session.SessionInfo;
using irsdkSharp.Serialization.Models.Session.SplitTimeInfo;
using irsdkSharp.Serialization.Models.Session.WeekendInfo;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using YamlDotNet.Serialization;

namespace irsdkSharp.Serialization.Models.Session
{
    public class IRacingSessionModel
    {
        public static IRacingSessionModel Serialize(string yaml)
        {
            yaml = PreprocessYAML(yaml);
            var r = new StringReader(yaml);
            var deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();
            try
            {
                return deserializer.Deserialize<IRacingSessionModel>(r);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
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

            var keyTracker = new KeyTracker[ keysToFix.Length ];

            for ( var i = 0; i < keyTracker.Length; i++ )
            {
                keyTracker[ i ] = new KeyTracker();
            }

            var stringBuilder = new StringBuilder( yaml.Length + keysToFix.Length * MaxNumAdditionalCharactersPerFixedKey * MaxNumDrivers );

            foreach ( var c in yaml )
            {
                for ( var i = 0; i < keysToFix.Length; i++ )
                {
                    if ( keyTracker[ i ].ignoreUntilNextLine )
                    {
                        if ( c == '\n' )
                        {
                            keyTracker[ i ].counter = 0;
                            keyTracker[ i ].ignoreUntilNextLine = false;
                        }
                    }
                    else if ( keyTracker[ i ].addFirstQuote )
                    {
                        if ( c != ' ' )
                        {
                            stringBuilder.Append( '\'' );

                            keyTracker[ i ].addFirstQuote = false;
                            keyTracker[ i ].addSecondQuote = true;
                        }
                    }
                    else if ( keyTracker[ i ].addSecondQuote )
                    {
                        if ( c == '\n' )
                        {
                            stringBuilder.Append( '\'' );

                            keyTracker[ i ].counter = 0;
                            keyTracker[ i ].addSecondQuote = false;
                        }
                        else if ( c == '\'' )
                        {
                            stringBuilder.Append( '\'' );
                        }
                    }
                    else
                    {
                        if ( c == keysToFix[ i ][ keyTracker[ i ].counter ] )
                        {
                            keyTracker[ i ].counter++;

                            if ( keyTracker[ i ].counter == keysToFix[ i ].Length )
                            {
                                keyTracker[ i ].addFirstQuote = true;
                            }
                        }
                        else if ( c != ' ' )
                        {
                            keyTracker[ i ].ignoreUntilNextLine = true;
                        }
                    }
                }

				stringBuilder.Append( c );
			}

			return stringBuilder.ToString();
        }
    }
}

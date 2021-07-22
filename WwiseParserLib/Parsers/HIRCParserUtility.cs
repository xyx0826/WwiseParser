using System;
using System.Collections.Generic;
using System.Diagnostics;
using WwiseParserLib.Structures.Objects.HIRC;

namespace WwiseParserLib.Parsers
{
    static class HIRCParserUtility
    {
        /// <summary>
        /// EventActionTypes that have custom settings.
        /// </summary>
        private static ISet<EventActionType> _typesWithSettings;

        private static void Initialize()
        {
            var types = Enum.GetNames(typeof(EventActionType));
            _typesWithSettings = new HashSet<EventActionType>(types.Length);

            // Populate set
            foreach (var type in types)
            {
                if (Attribute.IsDefined(
                    typeof(EventActionType).GetField(type),
                    typeof(HasSettingsAttribute)))
                {
                    _typesWithSettings.Add(
                        (EventActionType)Enum.Parse(typeof(EventActionType), type));
                }
            }
        }

        public static bool HasSettings(this EventActionType type)
        {
            if (_typesWithSettings == null)
            {
                Initialize();
            }

            Debug.Assert(_typesWithSettings != null, nameof(_typesWithSettings) + " != null");
            return _typesWithSettings.Contains(type);
        }
    }
}

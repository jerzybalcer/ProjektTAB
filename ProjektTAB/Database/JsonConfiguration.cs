using Database.Users;
using JsonSubTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public static class JsonConfiguration
    {
        public enum UserType
        {
            Doctor=1,
            LabAssistant=2,
            LabManager=3,
            Receptionist=4
        }
        public static  JsonSerializerSettings GetJsonSettings()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(JsonSubtypesConverterBuilder
                .Of<User>("Type") 
                .RegisterSubtype<Doctor>(UserType.Doctor)
                .RegisterSubtype<LabAssistant>(UserType.LabAssistant)
                .RegisterSubtype<LabManager>(UserType.LabManager)
                .RegisterSubtype<Receptionist>(UserType.Receptionist)
                .SerializeDiscriminatorProperty() 
                .Build());
            settings.TypeNameHandling = TypeNameHandling.All;
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return settings;
        }
    }
}

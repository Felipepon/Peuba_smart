using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]

public enum DocumentType
{
    IDCard,        
    Passport,      
    DriverLicense  
}
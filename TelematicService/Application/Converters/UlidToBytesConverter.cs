using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TelematicService.Application.Converters
{
    public class UlidToBytesConverter : ValueConverter<Ulid, byte[]>
    {
        public UlidToBytesConverter()
            : base(
                ulid => ulid.ToByteArray(),   // ULID → byte[]
                bytes => new Ulid(bytes)      // byte[] → ULID
            )
        { }
    }
}

using EdiFabric.Framework.Readers;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using EdiFabric.Templates.Hipaa5010;
using EdiFabric.Core.Model.Edi;
using EdiFabric;
using Newtonsoft.Json;
using AdiMenTestTask.Interfaces;
using System.Text;

namespace AdiMenTestTask
{
    public class Parse837Form : ParseForm
    {
        public string Parse(string input, Encoding? encoding = null)
        {
            SerialKey.Set(Helper.EdiFabricSerial);

            encoding ??= Encoding.UTF8;
            //var ediStream = System.IO.File.OpenRead(Directory.GetCurrentDirectory() + @"\Files\InstitutionalClaim1.txt");
            var stream = new MemoryStream(encoding.GetByteCount(input));
            using var writer = new StreamWriter(stream, encoding, -1, true);
            writer.Write(input);
            writer.Flush();
            stream.Position = 0;

            List<IEdiItem> ediItems;
            using (var ediReader = new X12Reader(stream, "EdiFabric.Templates.Hipaa"))
                ediItems = ediReader.ReadToEnd().ToList();
            var claims = ediItems.OfType<TS837I>();
            StringBuilder builder = new StringBuilder();
            builder.Append("{\"Claims\":[");
            var last = claims.Last();
            foreach (var claim in claims)
            {
                foreach (var loop2300 in claim.Loop2000A.First().Loop2000B.First().Loop2300)
                {
                    builder.Append(JsonConvert.SerializeObject(loop2300.CLM_ClaimInformation));
                    if(!claim.Equals(last)) builder.Append(',');
                }
            }
            builder.Append("]}");
            return builder.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using static WebSocketsTest.Program;
namespace WebSocketsTest
{
    public class Unit
    {

        public static byte[] Execute(bool result)
        {
            var binFormatter = new BinaryFormatter();
            var mStream = new MemoryStream();

            binFormatter.Serialize(mStream, Users);

            var byteArr = mStream.ToArray();

            if (!result)
            {
                return byteArr;
            }
            return default;
        }

        public static byte[] Execute (string result)
        {
            var binFormatter = new BinaryFormatter();
            var mStream = new MemoryStream();

            if (!string.IsNullOrEmpty(result))
            {

                
                if (!Users.Any(x => x.Value.Equals(result)))
                {
                    Users.Add(Users.Count + 1, result);

                    binFormatter.Serialize(mStream, true);

                    return mStream.ToArray();
                }
                else
                {
                    binFormatter.Serialize(mStream, false);

                    return mStream.ToArray();
                }
            }

            return default;
        }
    }
}

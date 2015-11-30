using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PicoStack.Core;
using PicoStack.Core.Web;

namespace PicoStack.Specs
{
    [TestFixture]
    public class HttpHeaderSpecs
    {
        [Test]
        public void ToString_outputs_valid_http_header_format()
        {
            var header = new HttpHeader("Connection", "close");
            Assert.AreEqual("Connection: close\r\n", header.ToString());
        }

        [Test]
        public void Parse_splits_line_into_name_and_value()
        {
            var line = "Connection: close\r\n";
            var header = HttpHeader.Parse(line);

            Assert.AreEqual("Connection", header.Name);
            Assert.AreEqual("close", header.Value);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ericmas001.Util
{
    /* http://onoffswitch.net/composable-regex/
    [Test]
    [TestCase("someDude@gmail.com", true)]
    [TestCase("foo", false)]
    [TestCase("foo@", false)]
    [TestCase("!@2001:0db8:85a3:0000:0000:8a2e:0370:7334", true)]
    [TestCase("!@2001:0db8:85a3:0000:0000:8a2e:0370:73345", false)]
    [TestCase("stupidM0nk3yA0l@aol.net", true)]
    [TestCase("stupidM0nk3yA0l@aol.net.net", true)]
    [TestCase("123+guy@domain", true)]
    [TestCase("@@domain", false)]
    [TestCase("!@ 2001:0db8:85a3:0000:0000:8a2e:0370:73345", false)]
    [TestCase(@"""Abc\@def""@example.com", true)]
    [TestCase("customer/department=shipping@example.com", true)]
    [TestCase("$A12345@example.com", true)]
    [TestCase("!def!xyz%abc@example.com", true)]
    [TestCase("_somename@example.com", true)]
    [TestCase("abc+\"foo=123$\"+test@example.com", true)]
    [TestCase("", false)]
    [TestCase("john@uk", true)]
    [TestCase("john.a@uk", true)]
    [TestCase("john.@uk", false)]
    [TestCase("john..@uk", false)]
    [TestCase(".john@uk", false)]
    [TestCase("\"a group\":cal@iamcalx.com", true)]
    [TestCase("\"a group\":@iamcalx.com", false)]
    [TestCase(":\"a group\":cal@iamcalx.com", false)]
    [TestCase("\"a group\":cal@192.168.1.1.1", false)]
    [TestCase("\"a group\":cal@192.168.1.1", true)]
    [TestCase("\"a group\":cal@192.168.1.100", true)]
    [TestCase("\"a group\":cal@192.168.1111.100", false)]
    public void Email(string email, bool pass)
    {
        var composableRegex = @"
 
        weirdChars = (!|-|\+|\\|\$|\^|~|#|%|\?|{|}|_|/|=)
        numbers = \d
        characters = [A-z]
        anyChars = (weirdChars|numbers|characters)
             
        lettersFollowedBySingleDot = (anyChars+\.anyChars+)
             
        names = anyChars|lettersFollowedBySingleDot
             
        onlyQuotableCharacters = @|\s
        quotedNames = ""(names|onlyQuotableCharacters)+""
 
        anyValidStart = (names|quotedNames)+
 
        group = (quotedNames:anyValidStart)|anyValidStart
 
        local = ^(group)
 
        ipv4 = ((\d{1,3}.){3}(\d{1,3}))
 
        ipv6Entry = ([a-f]|[A-F]|[0-9]){4}? ## group of 4 hex values
        ipv6 = ((ipv6Entry:){7}?ipv6Entry) ## 8 groups of ipv6 entries
 
        comAddresses = (characters+(\.characters+)*) ## stuff like a.b.c.d etc
        domain = (comAddresses|ipv6|ipv4)$ ## this has to be at the end
 
        (local)@(domain)";
 
        var regex = new Regexer(composableRegex).Regex;
 
        Console.Write(regex);
 
        if (pass)
        {
            Assert.IsTrue(Regex.IsMatch(email, regex));
        }
        else
        {
            Assert.IsFalse(Regex.IsMatch(email, regex));
        }
    }
     * */
    public class Regexer
    {
        public String Regex { get; private set; }

        public List<String> DebugTrace { get; private set; }

        public Regexer(string regex, bool includesComments = true, bool useDebug = true)
        {
            if (String.IsNullOrEmpty(regex))
            {
                throw new ArgumentNullException("regex", "Regex cannot be null");
            }

            DebugTrace = new List<string>();

            Parse(regex, includesComments, useDebug);
        }

        private void Parse(string regex, bool includeComments, bool useDebug)
        {
            var splits = regex.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (splits.Count() == 1)
            {
                Regex = splits.First();
                return;
            }

            var groups = (from item in splits.Take(splits.Count() - 1)
                          where !item.StartsWith("##")
                          let keyValueSplit = item.Split(new[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries)
                          let key = keyValueSplit.First().Trim()
                          let value = keyValueSplit.Last().Trim()
                          where !String.IsNullOrEmpty(key)
                          where !String.IsNullOrEmpty(value)
                          let regexWithoutComments = includeComments ? value.Split(new[] { "##" }, 2, StringSplitOptions.RemoveEmptyEntries).First().Trim() : value
                          select new { Key = key, Value = regexWithoutComments }).ToList();

            var final = splits.Last().Trim();

            Regex = final;

            for (int i = groups.Count - 1; i >= 0; i--)
            {
                var item = groups[i];
                Regex = Regex.Replace(item.Key, item.Value);
                if (useDebug)
                {
                    DebugTrace.Add(Regex);
                }
            }
        }
    }
}

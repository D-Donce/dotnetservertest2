using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Globalization;
using System.Text;
using System.Security.Cryptography;

namespace IPFS_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HashsController : ControllerBase
    {
        // GET: api/Hashs
        [HttpGet]
        public IEnumerable<string> Get()
        {
            StringBuilder blockdata = new StringBuilder();

            blockdata.Append("{'blockhash' : '");
            blockdata.Append(Find_BlockHash());
            blockdata.Append("','blockheader' : '");
            StreamReader SR = new StreamReader(Location_BlockHash_Header());
            blockdata.Append(SR.ReadToEnd()); SR.Close();
            blockdata.Append("'},");

            return new string[] { blockdata.ToString() };

            //return new string[] { attestation.ToString() };
            //new string[] { "key" , "value" };
        }

        // GET: api/Hashs/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            string[] files = Directory.GetFiles(Location_BlockHash_Body(), "*.txt");
            StringBuilder transaction_list = new StringBuilder();
            foreach (var file in files)
            {
                transaction_list.Append("{'user' : '");
                string[] abcde = file.Split("formain");
                transaction_list.Append(abcde[1]);
                transaction_list.Append("'},");
            }
            return transaction_list.ToString();
        }

        public string Find_BlockHash()
        {
            DateTime utcDate = DateTime.UtcNow;
            var culture = new CultureInfo("en-US");
            string[] a = utcDate.ToString(culture).Split(' '),
                b = a[1].Split(':');

            string rebuild_blockhash = SHA256Hash(a[0] + ' ' + b[0] + ':' + b[1] + ':' +
                    (Int16.Parse(b[2]) - Int16.Parse(b[2]) % Global.difficulty_level).ToString()
                    + ' ' + "AM");
            //임의로 AM-PM조정필요;;

            return rebuild_blockhash;
        }
        public string Find_BlockHash_Location(string rebuild_blockhash)
        {
            return @"C:\ipfs_directory\formain\" + rebuild_blockhash;
        }
        public string Location_BlockHash_Header()
        {
            return @"C:\ipfs_directory\formain\" + Find_BlockHash() + @"\Header.txt";
        }
        public string Location_BlockHash_Body()
        {
            return @"C:\ipfs_directory\formain\" + Find_BlockHash() + @"\Body";
        }
        private string SHA256Hash(string data)
        {
            SHA256 sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(data));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }

        Person attestation = new Person();
    }

    public class Person
    {
        public string hash_value { get; set; }
        public override string ToString()
        {
            Hashtable ht = new Hashtable();

            ht.Add("user", "blessoms201comOks071!!");

            //저장된 객체 출력하기

            //foreach (var user in ht.Keys) { Console.WriteLine("{0} : {1}", user, ht[user]); }

            return "{'user' : '" + ht["user"] + "" +
                "'}";
        }
    }

    public static class Global
    {
        public static string beforehash = null;

        //난의도 5 고정
        public static int difficulty_level = 5;
    }
}
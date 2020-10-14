﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.CsProj;
using System;
using System.Collections.Generic;
using System.Linq;
using JT1078.Protocol.Extensions;

namespace JT1078.Protocol.Benchmark
{
    [Config(typeof(JT1078SerializerConfig))]
    [MarkdownExporterAttribute.GitHub]
    [MemoryDiagnoser]
    public class JT1078SerializerContext
    {
        private byte[] JT1078Bytes;

        [Params(100, 10000, 100000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            JT1078Bytes = "303163648188113501123456781001300000016BB392DA0503840102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F202122232425262728292A2B2C2D2E2F303132333435363738393A3B3C3D3E3F404142434445464748494A4B4C4D4E4F505152535455565758595A5B5C5D5E5F606162636465666768696A6B6C6D6E6F707172737475767778797A7B7C7D7E7F808182838485868788898A8B8C8D8E8F909192939495969798999A9B9C9D9E9FA0A1A2A3A4A5A6A7A8A9AAABACADAEAFB0B1B2B3B4B5B6B7B8B9BABBBCBDBEBFC0C1C2C3C4C5C6C7C8C9CACBCCCDCECFD0D1D2D3D4D5D6D7D8D9DADBDCDDDEDFE0E1E2E3E4E5E6E7E8E9EAEBECEDEEEFF0F1F2F3F4F5F6F7F8F9FAFBFCFDFEFF000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F202122232425262728292A2B2C2D2E2F303132333435363738393A3B3C3D3E3F404142434445464748494A4B4C4D4E4F505152535455565758595A5B5C5D5E5F606162636465666768696A6B6C6D6E6F707172737475767778797A7B7C7D7E7F808182838485868788898A8B8C8D8E8F909192939495969798999A9B9C9D9E9FA0A1A2A3A4A5A6A7A8A9AAABACADAEAFB0B1B2B3B4B5B6B7B8B9BABBBCBDBEBFC0C1C2C3C4C5C6C7C8C9CACBCCCDCECFD0D1D2D3D4D5D6D7D8D9DADBDCDDDEDFE0E1E2E3E4E5E6E7E8E9EAEBECEDEEEFF0F1F2F3F4F5F6F7F8F9FAFBFCFDFEFF000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F202122232425262728292A2B2C2D2E2F303132333435363738393A3B3C3D3E3F404142434445464748494A4B4C4D4E4F505152535455565758595A5B5C5D5E5F606162636465666768696A6B6C6D6E6F707172737475767778797A7B7C7D7E7F808182838485868788898A8B8C8D8E8F909192939495969798999A9B9C9D9E9FA0A1A2A3A4A5A6A7A8A9AAABACADAEAFB0B1B2B3B4B5B6B7B8B9BABBBCBDBEBFC0C1C2C3C4C5C6C7C8C9CACBCCCDCECFD0D1D2D3D4D5D6D7D8D9DADBDCDDDEDFE0E1E2E3E4E5E6E7E8E9EAEBECEDEEEFF0F1F2F3F4F5F6F7F8F9FAFBFCFDFEFF000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F202122232425262728292A2B2C2D2E2F303132333435363738393A3B3C3D3E3F404142434445464748494A4B4C4D4E4F505152535455565758595A5B5C5D5E5F606162636465666768696A6B6C6D6E6F707172737475767778797A7B7C7D7E7F8081828384".ToHexBytes();
        }

        [Benchmark(Description = "JT1078Serializer")]
        public void JT1078SerializeTest()
        {
            for (int i = 0; i < N; i++)
            {
                JT1078Package jT1078Package = new JT1078Package();
                jT1078Package.Label1 = new JT1078Label1(0x81);
                jT1078Package.Label2 = new JT1078Label2(0x88);
                jT1078Package.SN = 0x1135;
                jT1078Package.SIM = "11234567810";
                jT1078Package.LogicChannelNumber = 0x01;
                jT1078Package.Label3 = new JT1078Label3(0x30);
                jT1078Package.Timestamp = 1562085874181;
                jT1078Package.Bodies = Enumerable.Range(0, 900).Select(s => (byte)(s + 1)).ToArray();
                var hex = JT1078Serializer.Serialize(jT1078Package);
            }
        }

        [Benchmark(Description = "JT1078Deserialize")]
        public void TestJT808_0x0200_Deserialize()
        {
            for (int i = 0; i < N; i++)
            {
                JT1078Package package = JT1078Serializer.Deserialize(JT1078Bytes);
            }
        }
    }

    public class JT1078SerializerConfig : ManualConfig
    {
        public JT1078SerializerConfig()
        {
            AddJob(Job.Default.WithGcServer(false).WithToolchain(CsProjCoreToolchain.NetCoreApp31).WithPlatform(Platform.AnyCpu));
        }
    }
}

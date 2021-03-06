﻿using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// tkhd
    /// </summary>
    public class TrackHeaderBox : FullBox, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// tkhd
        /// </summary>
        /// <param name="version"></param>
        /// <param name="flags"></param>
        public TrackHeaderBox(byte version, uint flags) : base("tkhd", version, flags)
        {
        }
        public ulong CreationTime { get; set; }
        public ulong ModificationTime { get; set; }
        public uint TrackID { get; set; }
        public uint Reserved1 { get; set; }
        public ulong Duration { get; set; }
        public uint[] Reserved2 { get; set; } = new uint[2];
        public short Layer { get; set; }
        public short AlternateGroup { get; set; }
        public bool TrackIsAudio { get; set; } = false;
        public ushort Reserved3 { get; set; }
        public int[] Matrix { get; set; } = new int[9] { 0x00010000, 0, 0, 0, 0x00010000, 0, 0, 0, 0x40000000 };
        public uint Width { get; set; }
        public uint Height { get; set; }
        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterFullBoxToBuffer(ref writer);
            if (Version == 1)
            {
                writer.WriteUInt64(CreationTime);
                writer.WriteUInt64(ModificationTime);
                writer.WriteUInt32(TrackID);
                writer.WriteUInt32(Reserved1);
                writer.WriteUInt64(Duration);
            }
            else
            {
                writer.WriteUInt32((uint)CreationTime);
                writer.WriteUInt32((uint)ModificationTime);
                writer.WriteUInt32(TrackID);
                writer.WriteUInt32(Reserved1);
                writer.WriteUInt32((uint)Duration);
            }
            foreach(var r in Reserved2)
            {
                writer.WriteUInt32(r);
            }
            writer.WriteInt16(Layer);
            writer.WriteInt16(AlternateGroup);
            if (TrackIsAudio)
            {
                writer.WriteInt16(0x0100);
            }
            else
            {
                writer.WriteInt16(0);
            }
            writer.WriteUInt16(Reserved3);
            foreach (var m in Matrix)
            {
                writer.WriteInt32(m);
            }
            writer.WriteUInt32(Width);
            writer.WriteUInt32(Height);
            End(ref writer);
        }
    }
}

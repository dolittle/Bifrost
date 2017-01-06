/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using System.Reflection;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents an implementation of <see cref="IAssemblyUtility"/>
    /// </summary>
    public class AssemblyUtility : IAssemblyUtility
    {
#pragma warning disable 1591 // Xml Comments
#pragma warning disable 0219 // Unused variables
        public bool IsAssembly(AssemblyInfo assemblyInfo)
        {
            if (string.IsNullOrEmpty(assemblyInfo.Path)) return true;

            // Borrowed from : http://stackoverflow.com/questions/8593264/determining-if-a-dll-is-a-valid-clr-dll-by-reading-the-pe-directly-64bit-issue
            using (var fs = new FileStream(assemblyInfo.Path, FileMode.Open, FileAccess.Read))
            {

                try
                {
                    BinaryReader reader = new BinaryReader(fs);
                    //PE Header starts @ 0x3C (60). Its a 4 byte header.
                    fs.Position = 0x3C;
                    uint peHeader = reader.ReadUInt32();
                    //Moving to PE Header start location...
                    fs.Position = peHeader;
                    uint peHeaderSignature = reader.ReadUInt32();
                    ushort machine = reader.ReadUInt16();
                    ushort sections = reader.ReadUInt16();
                    uint timestamp = reader.ReadUInt32();
                    uint pSymbolTable = reader.ReadUInt32();
                    uint noOfSymbol = reader.ReadUInt32();
                    ushort optionalHeaderSize = reader.ReadUInt16();
                    ushort characteristics = reader.ReadUInt16();

                    long posEndOfHeader = fs.Position;
                    ushort magic = reader.ReadUInt16();

                    int off = 0x60; // Offset to data directories for 32Bit PE images
                                    // See section 3.4 of the PE format specification.
                    if (magic == 0x20b) //0x20b == PE32+ (64Bit), 0x10b == PE32 (32Bit)
                    {
                        off = 0x70;  // Offset to data directories for 64Bit PE images
                    }
                    fs.Position = posEndOfHeader;

                    uint[] dataDictionaryRVA = new uint[16];
                    uint[] dataDictionarySize = new uint[16];
                    ushort dataDictionaryStart = Convert.ToUInt16(Convert.ToUInt16(fs.Position) + off);

                    fs.Position = dataDictionaryStart;

                    for (int i = 0; i < 15; i++)
                    {
                        dataDictionaryRVA[i] = reader.ReadUInt32();
                        dataDictionarySize[i] = reader.ReadUInt32();
                    }
                    if (dataDictionaryRVA[14] == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool IsAssemblyDynamic(Assembly assembly)
        {
            return assembly.IsDynamic;
        }
#pragma warning restore 0219 // Unused variables        
#pragma warning restore 1591 // Xml Comments
    }
}

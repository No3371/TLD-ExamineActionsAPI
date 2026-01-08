// #define VERY_VERBOSE
using System.Security.Cryptography;
using System.Text;
using System.IO.Compression;
using MelonLoader.ICSharpCode.SharpZipLib.Zip;
using MelonLoader.Utils;
using UnityEngine;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public static class ZippedActionDefLoader
	{
		internal static readonly List<byte[]> hashes = new();
		internal static List<DataDrivenGenericAction> cache;
		public static Task<List<DataDrivenGenericAction>> Run ()
		{
			return Task.Run(RunInternal);
		}
		public static List<DataDrivenGenericAction> RunInternal ()
		{
			cache = new();
			LoadFilesInDirectory(MelonEnvironment.ModsDirectory, false);
			ExamineActionsAPI.Instance.LoggerInstance.Msg($"Loaded {cache.Count} actions from files");
			return cache;
		}
		private static void LoadFilesInDirectory(string directory, bool recursive)
		{
			if (recursive)
			{
				string[] directories = Directory.GetDirectories(directory);
				foreach (string eachDirectory in directories)
				{
					LoadFilesInDirectory(eachDirectory, true);
				}
			}

			string[] files = Directory.GetFiles(directory, "*.eaapi");

			Array.Sort(files);

			foreach (string eachFile in files)
			{
				if (eachFile.ToLower().EndsWith(".eaapi"))
				{
					//PageManager.AddToItemPacksPage(new ItemPackData(eachFile));
					try
					{
						LoadEAAPIFile(eachFile);
					}
					catch (Exception e)
					{
						ExamineActionsAPI.Instance.LoggerInstance.Error($"Error Loading .modcomponent file\n'{e}'");
					}
				}
			}
		}
		private static void LoadEAAPIFile(string filePath)
		{
			string fileName = Path.GetFileName(filePath);
			string fileNameNoExt = Path.GetFileNameWithoutExtension(filePath);

			using (ZipArchive archive = System.IO.Compression.ZipFile.OpenRead(filePath))
			{
				foreach (ZipArchiveEntry entry in archive.Entries)
				{
					string internalPath = entry.Name;
					string filename = Path.GetFileName(internalPath);

					using Stream zipInputStream = entry.Open();
					using MemoryStream unzippedFileStream = new MemoryStream();
					int size = 0;
					byte[] buffer = new byte[4096];
					while (true)
					{
						size = zipInputStream.Read(buffer, 0, buffer.Length);
						if (size > 0)
						{
							unzippedFileStream.Write(buffer, 0, size);
						}
						else
						{
							break;
						}
					}
					var fileContent = ReadToJsonString(unzippedFileStream);
					try
					{
						var act = DataDrivenGenericAction.NewWithJson(fileContent);
						cache.Add(act);
					}
					catch (Exception ex)
					{
						ExamineActionsAPI.Instance.LoggerInstance.Error($"Failed to create action from {filePath} -> {filename}: ", ex);
					}
				}
			}
		}

		private static string ReadToJsonString(MemoryStream memoryStream)
		{
			const byte leftCurlyBracket = (byte)'{';
			byte[] bytes = memoryStream.ToArray();
			int index = Array.IndexOf(bytes, leftCurlyBracket);
			if (index < 0)
			{
				throw new ArgumentException("MemoryStream has no Json content.", nameof(memoryStream));
			}
			return Encoding.UTF8.GetString(new ReadOnlySpan<byte>(bytes, index, bytes.Length - index));
		}
	}
}
// https://github.com/dommrogers/ModComponent/blob/3216d012d22a6af14b9f3d0e5591b4f417fa2388/ModComponent/Utils/FileUtils.cs
internal static class FileUtils
{

	private static Dictionary<string, byte[]> headerTypes = new() {
		{ "zip-1", new byte[] { 0x50, 0x4b, 0x03, 0x04 } },
		{ "zip-2", new byte[] { 0x50, 0x4b, 0x05, 0x06 } },
		{ "zip-3", new byte[] { 0x50, 0x4b, 0x07, 0x08 } },
		{ "gzip-1", new byte[] { 0x1f, 0x8b }},
		{ "tar-1", new byte[] { 0x1f, 0x9d }},
		{ "lzh-1", new byte[] { 0x1f, 0xa0 }},
		{ "bzip-1", new byte[] { 0x42, 0x5a, 0x68 }},
		{ "lzip-1", new byte[] { 0x4c, 0x5a, 0x49, 0x50 }},
		{ "rar-1", new byte[] { 0x52, 0x61, 0x72, 0x21, 0x1a }},
		{ "7z-1", new byte[] { 0x37, 0x7a, 0xbc, 0xaf, 0x27 }},
	};


	private static byte[] GetFirstBytes(FileStream fs, int length)
	{
		fs.Seek(0, 0);
		var bytes = new byte[length];
		fs.Read(bytes, 0, length);
		return bytes;
	}

	internal static string? DetectZipFileType(FileStream fs)
	{
		var data = GetFirstBytes(fs, 5);

		foreach (KeyValuePair<string, byte[]> headerType in headerTypes)
		{
			if (HeaderBytesMatch(headerType, data))
			{
				return headerType.Key.ToLowerInvariant().Split('-')[0];
			}
		}
		return null;
	}

	private static bool HeaderBytesMatch(KeyValuePair<string, byte[]> headerType, byte[] dataBytes)
	{
		if (dataBytes.Length < headerType.Value.Length)
		{
			return false;
		}

		for (var i = 0; i < headerType.Value.Length; i++)
		{
			if (headerType.Value[i] == dataBytes[i]) continue;

			return false;
		}
		return true;
	}

	internal static string GetRelativePath(string file, string directory)
	{
		if (file.StartsWith(directory))
		{
			return file.Substring(directory.Length + 1);
		}

		throw new ArgumentException("Could not determine relative path of '" + file + "' to '" + directory + "'.");
	}
}
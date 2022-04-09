using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace modul7_1302204066
{
    internal class BankTransferConfig
    {
        public String Language { get; set; }

        public int Threshold { get; set; }

        public int LowFee { get; set; }

        public int HighFee { get; set; }

        public List<String> Methods { get; set; }

        public String Confirmation { get; set; }

        public BankTransferConfig()
        {
			Methods = new List<string>();
			ReadFile();
		}

		private string GetFilePath => Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "bank_transfer_config.json");

		public void ReadFile()
        {
			var file = File.OpenText(GetFilePath);

			JsonElement json = JsonSerializer.Deserialize<JsonElement>(file.ReadToEnd());

			Language = json.GetProperty("lang").GetString();
            Threshold = json.GetProperty("transfer").GetProperty("threshold").GetInt32();
            LowFee = json.GetProperty("transfer").GetProperty("low_fee").GetInt32();
            HighFee = json.GetProperty("transfer").GetProperty("high_fee").GetInt32();
            Confirmation = json.GetProperty("confirmation").GetProperty(Language).GetString();

			foreach (var item in json.GetProperty("methods").EnumerateArray().ToList())
			{
				Methods.Add(item.GetString());
			}
		}

		public void Transfer()
		{
			// no i pesan
			if (Language == "en")
			{
				Console.WriteLine("Please insert the amount of money to transfer");
			}
			else
			{
				Console.WriteLine("Masukkan jumlah uang yang akan di-transfer:");
			}
			string rawTransfer = Console.ReadLine();
			int transfer = int.Parse(rawTransfer);
			int biayaTransfer;
			int totalBiaya;
			if (transfer <= Threshold)
			{
				biayaTransfer = LowFee;
			}
			else
			{
				biayaTransfer = HighFee;
			}

			totalBiaya = transfer + biayaTransfer;

			if (Language == "en")
			{
				Console.WriteLine($"Transfer fee: {biayaTransfer}");
				Console.WriteLine($"Total amount: {totalBiaya}");

				Console.WriteLine("\nSelect transfer method: ");
			}
			else
			{
				Console.WriteLine($"Biaya transfer: {biayaTransfer}");
				Console.WriteLine($"Total biaya: {totalBiaya}");

				Console.WriteLine("\nPilih metode transfer: ");
			}

			for (int i = 0; i < Methods.Count; i++)
			{
				Console.WriteLine($"{i + 1}. {Methods[i]}");
			}
			string method = Console.ReadLine();

			if (Language == "en")
			{
				Console.WriteLine($"Please type {Confirmation}");
			}
			else
			{
				Console.WriteLine($"Ketik {Confirmation}");
			}
			string confirm = Console.ReadLine();

			if (confirm == Confirmation)
			{
				if (Language == "en")
				{
					Console.WriteLine("The transfer is completed");
				}
				else
				{
					Console.WriteLine("Proses transfer berhasil");
				}
			}
			else
			{
				if (Language == "en")
				{
					Console.WriteLine("Transfer is cancelled");
				}
				else
				{
					Console.WriteLine("Transfer dibatalkan");
				}
			}

		}

	}
}

﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Citrine.Core.Api;

namespace Citrine.Core.Modules
{
	public partial class FortuneModule : ModuleBase
	{
		public async override Task<bool> ActivateAsync(IPost n, IShell shell, Server core)
		{
			if (n.Text != null && Regex.IsMatch(n.Text.ToLowerInvariant(), "占|運勢|みくじ|fortune"))
			{
				var r = new Random(n.User.Id.GetHashCode() + DateTime.Now.Day + DateTime.Now.Month - DateTime.Now.Year);

				int love = r.Next(1, 6),
					money = r.Next(1, 6),
					work = r.Next(1, 6),
					study = r.Next(1, 6);

				var result = Math.Min((love + money + work + study) / 2 - 1, results.Length - 1);

				var builder = new StringBuilder();

				builder.AppendLine($"***{results[result]}***");
				builder.AppendLine($"恋愛運❤: {GetStar(love, 5)}");
				builder.AppendLine($"金運💰: {GetStar(money, 5)}");
				builder.AppendLine($"仕事💻: {GetStar(work, 5)}");
				builder.AppendLine($"勉強📒: {GetStar(study, 5)}");
				builder.AppendLine($"ラッキーアイテム💎: {itemPrefixes.Random(r)}{items.Random(r)}");

				await shell.ReplyAsync(n, builder.ToString(), $"僕が今日の{(n.User.Name ?? n.User.ScreenName)}さんの運勢を占ったよ:");

				return true;
			}

			return false;
		}

		static string GetStar(int value, int maxValue) => new string('★', value) + new string('☆', maxValue - value);
	}

	public class AdminModule : ModuleBase
	{
		public override async Task<bool> ActivateAsync(IPost n, IShell shell, Server core)
		{
			if (n.Text == null)
				return false;

			if (n.Text.Contains("再起動"))
			{
				if (core.IsAdmin(n.User))
				{
					await shell.ReplyAsync(n, "またねー。");
					// good bye
					Environment.Exit(0);
				}
				else
				{
					var mes = core.GetRatingOf(n.User) == Rating.Partner ? "いくらあなたでも, その頼みだけは聞けない. ごめんね..." : "申し訳ないけど, 他の人に言われてもするなって言われてるから...";
					await shell.ReplyAsync(n, mes);
				}
				return true;
			}

			return false;
		}
	}

}
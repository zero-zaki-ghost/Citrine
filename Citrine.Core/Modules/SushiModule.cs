using System;
using System.Threading.Tasks;
using Citrine.Core.Api;

namespace Citrine.Core.Modules
{
	public class SushiModule : ModuleBase
	{
		public override async Task<bool> ActivateAsync(IPost n, IShell shell, Server core)
		{
			if (n.Text is string text && text.IsMatch("寿司(握|にぎ)"))
			{
				var res = "";
				var s = random.Next(10) > 3 ? null : sushi.Random();
				var max = random.Next(1, 10);
				for (var i = 0; i < max; i++)
					res += s ?? sushi.Random();
				await shell.ReplyAsync(n, "ヘイお待ち! " + res);
				return true;
			}
			return false;
		}

		private readonly Random random = new Random();

		private readonly string[] sushi =
		{
			"🍣", "🍣", "🍣", "🍣", "🍣", "🍣", "🍕", "🍔", "🍱", "🍘", "🍫", "📱", "💻",
		};
	}
}
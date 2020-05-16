﻿using Microsoft.Xna.Framework;
using OTAPI.Patcher.Engine.Extensions;
using OTAPI.Patcher.Engine.Modification;
using System.Collections.Generic;

namespace OTAPI.Modifications.NetworkText.Modifications
{
	public class SendChatMessageToClientModification : ModificationBase
	{
		public override IEnumerable<string> AssemblyTargets => new[]
		{
			"TerrariaServer, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null",
		};

		public override string Description => "Hooking Terraria.NetMessage.SendChatMessageToClient";

		public override void Run()
		{
			Color tmpColor = new Color();
			int tmpInt = 0;
			var sendChatMessage = Method<Terraria.NetMessage>("SendChatMessageToClient", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
			var sendChatMessageBeforeCallback = Method(() => Callbacks.Terraria.NetMessage.BeforeSendChatMessageToClient(null, ref tmpColor, ref tmpInt));
			var sendChatMessageAfterCallback = Method(() => Callbacks.Terraria.NetMessage.AfterSendChatMessageToClient(null, ref tmpColor, ref tmpInt));

			sendChatMessage.Wrap(sendChatMessageBeforeCallback, 
				sendChatMessageAfterCallback,
				beginIsCancellable: true,
				noEndHandling: true,
				allowCallbackInstance: false);

		}
	}
}

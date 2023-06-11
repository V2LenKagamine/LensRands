namespace LensRands.Systems.Networking
{
    public class LensPackets
    {
        /*
         * Not used, but example
        public readonly struct BungusPacket : IEasyPacket<BungusPacket>,IEasyPacketHandler<BungusPacket>
        {

            public readonly int playerID;
            public readonly bool bungus;


            public BungusPacket(int playerID,bool bungus) 
            {
                this.playerID = playerID;
                this.bungus = bungus;
            }
            public BungusPacket Deserialise(BinaryReader reader, in SenderInfo sender)
            {
                return new BungusPacket(reader.ReadInt32(), reader.ReadBoolean());
            }

            public void Receive(in BungusPacket packet, in SenderInfo sender, ref bool handled)
            {
                Main.NewText("Packet Received");
                Main.player[packet.playerID].GetModPlayer<LensPlayer>().NearBungus = packet.bungus;
                handled = true;
            }

            public void Serialise(BinaryWriter writer)
            {
                writer.Write(playerID);
                writer.Write(bungus);
            }
        }
        */

    }
}

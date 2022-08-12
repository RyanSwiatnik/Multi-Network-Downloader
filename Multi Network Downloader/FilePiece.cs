namespace Multi_Network_Downloader
{
    class FilePiece
    {
        public PieceState state = PieceState.Downloading;

        private byte[] data;

        public void setData(byte[] data)
        {
            state = PieceState.Downloaded;
            this.data = data;
        }

        public byte[] getData()
        {
            state = PieceState.Saving;
            return data;
        }

        public void setSaved()
        {
            state = PieceState.Saved;
            data = null;
        }
    }

    enum PieceState
    {
        Downloading,
        Downloaded,
        Saving,
        Saved
    }
}
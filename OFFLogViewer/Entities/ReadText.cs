namespace OFF.LogViewer.Entities
{

    /// <summary>
    ///     Считанный текст
    /// </summary>
    public struct ReadText
    {
        #region Fields

        /// <summary>
        ///     Начало текста в файле
        /// </summary>
        public int Begin;

        /// <summary>
        ///     Конец текста в файле
        /// </summary>
        public int End;

        /// <summary>
        ///     Считанный текст
        /// </summary>
        public string Text;

        #endregion

        #region Constructors

        public ReadText(string text, int begin, int end)
        {
            Text = text;
            Begin = begin;
            End = end;
        }

        #endregion
    }

}
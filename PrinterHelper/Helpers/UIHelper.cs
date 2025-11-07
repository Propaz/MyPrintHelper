using System.Windows.Forms;

namespace PrinterHelper.Helpers
{
    /// <summary>
    /// Вспомогательный класс для общих элементов пользовательского интерфейса.
    /// </summary>
    internal static class UIHelper
    {
        /// <summary>
        /// Отображает стандартизированное диалоговое окно с сообщением об ошибке.
        /// </summary>
        /// <param name="message">Текст ошибки для отображения.</param>
        public static void ShowError(string message)
        {
            MessageBox.Show(text: message, caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
        }
    }
}
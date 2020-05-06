using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Forms;

namespace EncryptionUserAndPassword
{
    public partial class MainWindow : Window
    {
        private string strKey = "A!9HHhi%XjjYY4YP2@Nob009X";
        string strResult = "";

        RSACryptoServiceProvider RSAService = new RSACryptoServiceProvider();

        public string Result
        {
            get => strResult;
            set => strResult = value;
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        public string Encrypt(string strTextToEncrypt)
        {
            using (MD5CryptoServiceProvider md5Encrypt = new MD5CryptoServiceProvider())
            {
                using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5Encrypt.ComputeHash(UTF8Encoding.UTF8.GetBytes(strKey));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (ICryptoTransform transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(strTextToEncrypt);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, 
                            textBytes.Length);

                        if (bytes.Length == 0 || bytes == null) return null;

                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        //From here begins the async Encryptation

        //LLAVE PÚBLICA
        public byte[] EncryptDataForPublicKey()
        {
            string strXMLPublicKey = RSAService.ToXmlString(false);

            if (string.IsNullOrEmpty(strXMLPublicKey) || string.IsNullOrWhiteSpace(strXMLPublicKey)) 
                return null;

            return Encoding.ASCII.GetBytes(strXMLPublicKey);
        }

        public void CreatePublicKeyWithTheDataEncrypted()
        {
            string strPathForTheKey = "";
            byte[] arrEncryptedDataForPublicKey;

            CommonOpenFileDialog fileDialog = new CommonOpenFileDialog()
            {
                IsFolderPicker = true,
                DefaultDirectory = "C:\\",
                Title = "Select the destiny for the key"
            };

            if (fileDialog.ShowDialog() == CommonFileDialogResult.Ok && fileDialog.FileName != "")
            {
                strPathForTheKey = fileDialog.FileName;

                FileStream fileStream = new FileStream(Path.Combine(strPathForTheKey, "PublicKey.xml"),
                    FileMode.Create, FileAccess.Write);

                if (fileStream == null) return;

                arrEncryptedDataForPublicKey = EncryptDataForPublicKey();
                fileStream.Write(arrEncryptedDataForPublicKey, 0, arrEncryptedDataForPublicKey.Length);
                fileStream.Close();
            }
        }

        //LLAVE PRIVADA
        public byte[] EncryptDataForPrivateKey()
        {
            string strXMLPrivateKey = RSAService.ToXmlString(true);

            if (string.IsNullOrEmpty(strXMLPrivateKey) || string.IsNullOrWhiteSpace(strXMLPrivateKey))
                return null;

            return Encoding.ASCII.GetBytes(strXMLPrivateKey);
        }

        public void CreatePrivateKeyWithTheDataEncrypted()
        {
            string strPathForTheKey = "";
            byte[] arrEncryptedDataForPrivateKey;

            CommonOpenFileDialog fileDialog = new CommonOpenFileDialog()
            {
                IsFolderPicker = true,
                DefaultDirectory = "C:\\",
                Title = "Select the destiny for the key"
            };

            if (fileDialog.ShowDialog() == CommonFileDialogResult.Ok && fileDialog.FileName != "")
            {
                strPathForTheKey = fileDialog.FileName;

                FileStream fileStream = new FileStream(Path.Combine(strPathForTheKey, "PrivateKey.xml"),
                    FileMode.Create, FileAccess.Write);

                if (fileStream == null) return;

                arrEncryptedDataForPrivateKey = EncryptDataForPrivateKey();
                fileStream.Write(arrEncryptedDataForPrivateKey, 0, arrEncryptedDataForPrivateKey.Length);
                fileStream.Close();
            }
        }

        //ENCRIPTACIÓN
        public byte[] EncryptDataUsingTheXMLKeyGenerated(string strTextToEncrypt, string strXMLKey)
        {
            if (string.IsNullOrEmpty(strTextToEncrypt) || string.IsNullOrWhiteSpace(strXMLKey)) 
                return null;

            RSAService = new RSACryptoServiceProvider(1024);
            RSAService.FromXmlString(strXMLKey);
            byte[] arrEncryptedText = RSAService.Encrypt(
                Encoding.ASCII.GetBytes(strTextToEncrypt), false);

            if (arrEncryptedText.Length == 0 || arrEncryptedText == null) return null;

            return arrEncryptedText;
        }

        public string EncryptText(string strTextToEncrypt)
        {
            if (string.IsNullOrEmpty(strTextToEncrypt)) return "";

            //CommonOpenFileDialog fileDialog = new CommonOpenFileDialog()
            //{
            //    IsFolderPicker = false,
            //    DefaultDirectory = "C:\\",
            //    Title = "Select the key",
            //    Multiselect = false,
            //    //DefaultExtension = ".xml"
            //};

            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "Public or Private Key in XML (*.xml)|*.xml"
            };

            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
                openFile.FileName != "")
            {
                Stream stream = openFile.OpenFile();

                if (stream != null)
                {
                    string strXMLFile = new StreamReader(stream).ReadToEnd();
                    byte[] arrEncryptedText = EncryptDataUsingTheXMLKeyGenerated(
                        strTextToEncrypt, strXMLFile);
                    Result = Convert.ToBase64String(arrEncryptedText);
                }
                else
                    return "";
            }

            return Result;
        }

        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            //Result = Encrypt(txtTextToEncrypt.Text);

            //System.Windows.MessageBox.Show(Result);

            //Este método va a pedir un path donde guardará la llave publica que se usará para encriptar
            //CreatePublicKeyWithTheDataEncrypted();
            CreatePrivateKeyWithTheDataEncrypted();
            //Y luego pedirá la ubicación de la llave para ver la encriptación
            EncryptText(txtTextToEncrypt.Text);
            txtOutput.Text = Result;
            System.Windows.MessageBox.Show(Result);
        }

        private void btnGeneratePrivate_Click(object sender, RoutedEventArgs e)
        {
            CreatePrivateKeyWithTheDataEncrypted();
        }
    }
}

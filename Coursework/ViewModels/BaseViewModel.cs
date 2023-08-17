using System;
using System.Text.Json.Serialization;
using System.Windows.Input;
using System.Xml;
using Coursework.Services;
using Microsoft.Win32;
using System.IO;
using System.Collections.ObjectModel;



namespace Coursework.ViewModels

{
    public class BaseMainWindowViewModel : BaseViewModel
    {
        private string _messageHeader;
        private string _messageBody;
        private string _processedMessage;
        private string _feedbackMessage;
        public ObservableCollection<string> TrendingHashtags { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> Mentions { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> SIRList { get; } = new ObservableCollection<string>();


        public string MessageHeader
        {
            get => _messageHeader;
            set
            {
                _messageHeader = value;
                OnPropertyChanged();
            }
        }

        public string MessageBody
        {
            get => _messageBody;
            set
            {
                _messageBody = value;
                OnPropertyChanged();
            }
        }

        public string ProcessedMessage
        {
            get => _processedMessage;
            set
            {
                _processedMessage = value;
                OnPropertyChanged();
            }
        }

        public string FeedbackMessage
        {
            get => _feedbackMessage;
            set
            {
                _feedbackMessage = value;
                OnPropertyChanged();
            }
        }

        public ICommand ProcessMessageCommand { get; }
        public ICommand LoadFromFileCommand { get; }

        private readonly MessageProcessorService _messageProcessorService;

        public ICommand ProcessMessageCommand => new RelayCommand(ProcessMessage);


        public BaseMainWindowViewModel()
        {
            _messageProcessorService = new MessageProcessorService();

            ProcessMessageCommand = new RelayCommand(ProcessMessage);
            LoadFromFileCommand = new RelayCommand(LoadFromFile);
        }

        private void ProcessMessage()
        {
            try
            {
                // Use the MessageProcessorService to process the message
                var processedMessage = _messageProcessorService.ProcessMessage(MessageHeader, MessageBody);

                // Convert the processed message to a displayable format (e.g., JSON)
                ProcessedMessage = JsonConvert.SerializeObject(processedMessage, Formatting.Indented);

                // Clear any previous feedback messages
                FeedbackMessage = string.Empty;
            }
            catch (Exception ex)
            {
                FeedbackMessage = $"Error: {ex.Message}";
            }
        }


        private void LoadFromFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string fileContent = File.ReadAllText(openFileDialog.FileName);
                // Assuming the file contains two lines: MessageHeader and MessageBody
                var lines = fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                if (lines.Length >= 2)
                {
                    MessageHeader = lines[0];
                    MessageBody = lines[1];
                }
            }
        }

    }
}

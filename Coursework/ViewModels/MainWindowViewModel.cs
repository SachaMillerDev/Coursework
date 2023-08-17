using System;
using System.Text.Json.Serialization;
using System.Windows.Input;
using System.Xml;
using Coursework.Services;
using Microsoft.Win32;
using System.IO;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Coursework.Commands;

namespace Coursework.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string _messageHeader;
        private string _messageBody;
        private string _processedMessage;
        private string _feedbackMessage;
        private readonly MessageProcessorService _messageProcessorService;

        public ObservableCollection<string> TrendingHashtags { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> Mentions { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> SIRList { get; } = new ObservableCollection<string>();

        public MainWindowViewModel()
        {
            var textspeakService = new TextspeakService("Data/TextspeakAbbreviations.csv"); // Provide the correct path to your CSV file
            _messageProcessorService = new MessageProcessorService(textspeakService);
        }


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

        public ICommand ProcessMessageCommand => new RelayCommand(_ => ProcessMessage());
        public ICommand LoadFromFileCommand => new RelayCommand(_ => LoadFromFile());

        private void ProcessMessage()
        {
            try
            {
                var processedMessage = _messageProcessorService.ProcessMessage(MessageHeader, MessageBody);
                ProcessedMessage = JsonConvert.SerializeObject(processedMessage, Newtonsoft.Json.Formatting.Indented);
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

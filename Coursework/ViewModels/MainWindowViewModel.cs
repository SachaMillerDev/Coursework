﻿using System;
using System.Text.Json.Serialization;
using System.Windows.Input;
using System.Xml;
using Coursework.Services;

namespace Coursework.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string _messageHeader;
        private string _messageBody;
        private string _processedMessage;
        private string _feedbackMessage;

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

        public MainWindowViewModel()
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
                // Display any errors as feedback to the user
                FeedbackMessage = ex.Message;
            }
        }


        private void LoadFromFile()
        {
            // TODO: Implement the logic to load a message from a file
        }
    }
}

﻿using System;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Config;

namespace Intersect.Client.UI.Game.Chat
{
    public class Chatbox
    {
        private ComboBox mChannelCombobox;
        private TextBox mChatboxInput;
        private ListBox mChatboxMessages;
        private ScrollBar mChatboxScrollBar;
        private Label mChatboxText;

        private Button mChatboxSendButton;

        //Window Controls
        private ImagePanel mChatboxWindow;

        private GameGuiBase mGameUi;
        private int mMessageIndex;
        private bool mReceivedMessage;

        //Init
        public Chatbox(Canvas gameCanvas, GameGuiBase gameUi)
        {
            mGameUi = gameUi;

            //Chatbox Window
            mChatboxWindow = new ImagePanel(gameCanvas, "ChatboxWindow");
            mChatboxMessages = new ListBox(mChatboxWindow, "MessageList");
            mChatboxMessages.EnableScroll(false, true);
            mChatboxWindow.ShouldCacheToTexture = true;

            mChatboxInput = new TextBox(mChatboxWindow, "ChatboxInputField");
            mChatboxInput.SubmitPressed += ChatBoxInput_SubmitPressed;
            mChatboxInput.Text = GetDefaultInputText();
            mChatboxInput.Clicked += ChatBoxInput_Clicked;
            mChatboxInput.IsTabable = false;
            Gui.FocusElements.Add(mChatboxInput);

            mChannelCombobox = new ComboBox(mChatboxWindow, "ChatChannelCombobox");
            for (int i = 0; i < 3; i++)
            {
                var menuItem = mChannelCombobox.AddItem(Strings.Chatbox.channels[i]);
                menuItem.UserData = i;
            }
            //Add admin channel only if power > 0.
            if (Globals.Me.Type > 0)
            {
                var menuItem = mChannelCombobox.AddItem(Strings.Chatbox.channeladmin);
                menuItem.UserData = 3;
            }

            mChatboxText = new Label(mChatboxWindow);
            mChatboxText.Name = "ChatboxText";
            mChatboxText.Font = mChatboxWindow.Parent.Skin.DefaultFont;

            mChatboxSendButton = new Button(mChatboxWindow, "ChatboxSendButton");
            mChatboxSendButton.Text = Strings.Chatbox.send;
            mChatboxSendButton.Clicked += ChatBoxSendBtn_Clicked;

            mChatboxWindow.LoadJsonUi(GameContentManager.UI.InGame, GameGraphics.Renderer.GetResolutionString());

            mChatboxText.IsHidden = true;
        }

        //Update
        public void Update()
        {
            if (mReceivedMessage)
            {
                mChatboxMessages.ScrollToBottom();
                mReceivedMessage = false;
            }

            var msgs = ChatboxMsg.GetMessages();
            for (var i = mMessageIndex; i < msgs.Count; i++)
            {
                var msg = msgs[i];
                var myText = Gui.WrapText(msg.GetMessage(),
                    mChatboxMessages.Width - mChatboxMessages.GetVerticalScrollBar().Width - 8,
                    mChatboxText.Font);
                foreach (var t in myText)
                {
                    var rw = mChatboxMessages.AddRow(t.Trim());
                    rw.SetTextFont(mChatboxText.Font);
                    rw.SetTextColor(msg.GetColor());
                    rw.ShouldDrawBackground = false;
                    rw.UserData = msg.GetTarget();
                    rw.Clicked += ChatboxRow_Clicked;
                    mReceivedMessage = true;

                    while (mChatboxMessages.RowCount > ClientOptions.ChatLines)
                    {
                        mChatboxMessages.RemoveRow(0);
                    }
                }
                mMessageIndex++;
            }
        }

        public void SetChatboxText(string msg)
        {
            mChatboxInput.Text = msg;
            mChatboxInput.Focus();
        }

        private void ChatboxRow_Clicked(Base sender, ClickedEventArgs arguments)
        {
            ListBoxRow rw = (ListBoxRow) sender;
            string target = (string) rw.UserData;
            if (target != "")
            {
                if (mGameUi.AdminWindowOpen())
                {
                    mGameUi.AdminWindowSelectName(target);
                }
            }
        }

        //Extra Methods
        public void Focus()
        {
            if (!mChatboxInput.HasFocus)
            {
                mChatboxInput.Focus();
                mChatboxInput.Text = "";
            }
        }

        //Input Handlers
        //Chatbox Window
        void ChatBoxInput_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (mChatboxInput.Text == GetDefaultInputText())
            {
                mChatboxInput.Text = "";
            }
        }

        void ChatBoxInput_SubmitPressed(Base sender, EventArgs arguments)
        {
            TrySendMessage();
        }

        void ChatBoxSendBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {
            TrySendMessage();
        }

        void TrySendMessage()
        {
            if (mChatboxInput.Text.Trim().Length <= 0 || mChatboxInput.Text == GetDefaultInputText())
            {
                mChatboxInput.Text = GetDefaultInputText();
                return;
            }

            PacketSender.SendChatMsg(mChatboxInput.Text.Trim(), (int) mChannelCombobox.SelectedItem.UserData);
            mChatboxInput.Text = GetDefaultInputText();
        }

        string GetDefaultInputText()
        {
            var key1 = GameControls.ActiveControls.ControlMapping[Controls.Enter].Key1;
            var key2 = GameControls.ActiveControls.ControlMapping[Controls.Enter].Key2;
            if (key1 == Keys.None && key2 != Keys.None)
            {
                return Strings.Chatbox.enterchat1.ToString(Strings.Keys.keydict[Enum.GetName(typeof(Keys), key2).ToLower()]);
            }
            else if (key1 != Keys.None && key2 == Keys.None)
            {
                return Strings.Chatbox.enterchat1.ToString(Strings.Keys.keydict[Enum.GetName(typeof(Keys), key1).ToLower()]);
            }
            else if (key1 != Keys.None && key2 != Keys.None)
            {
                return Strings.Chatbox.enterchat1.ToString(Strings.Keys.keydict[Enum.GetName(typeof(Keys), key1).ToLower()], Strings.Keys.keydict[Enum.GetName(typeof(Keys), key2).ToLower()]);
            }
            return Strings.Chatbox.enterchat;
        }
    }
}
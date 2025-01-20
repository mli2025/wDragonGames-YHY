import React, { useEffect, useState } from 'react';
import { loadMessages, saveMessages } from '../utils/storage';

const Chat = () => {
  const [messages, setMessages] = useState([]);

  useEffect(() => {
    const initializeMessages = async () => {
      const savedMessages = await loadMessages();
      if (savedMessages && savedMessages.length > 0) {
        setMessages(savedMessages);
      }
    };
    
    initializeMessages();
  }, []);

  useEffect(() => {
    if (messages.length > 0) {
      saveMessages(messages);
    }
  }, [messages]);

  return (
    <div>
      {/* Render your chat components here */}
    </div>
  );
};

export default Chat; 
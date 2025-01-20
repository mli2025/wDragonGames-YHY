import path from 'path';
import fs from 'fs/promises';
import { SyncService } from 'xiehouyu.shared';

const ensureDirectoryExists = async () => {
  const dir = path.join(process.cwd(), 'data');
  try {
    await fs.access(dir);
  } catch {
    await fs.mkdir(dir, { recursive: true });
  }
};

export const saveMessages = async (messages) => {
  await ensureDirectoryExists();
  try {
    const filePath = path.join(process.cwd(), 'data', 'messages.json');
    await fs.writeFile(filePath, JSON.stringify(messages, null, 2));
    
    await SyncService.syncMessages(messages);
    
    console.log('Messages saved and synced successfully');
  } catch (error) {
    console.error('Error saving messages:', error);
  }
};

export const loadMessages = async () => {
  try {
    const filePath = path.join(process.cwd(), 'data', 'messages.json');
    const localData = await fs.readFile(filePath, 'utf8');
    const localMessages = JSON.parse(localData);
    
    const remoteMessages = await SyncService.getLatestMessages();
    
    const mergedMessages = mergeMessages(localMessages, remoteMessages);
    
    return mergedMessages;
  } catch (error) {
    console.error('Error loading messages:', error);
    return [];
  }
}; 
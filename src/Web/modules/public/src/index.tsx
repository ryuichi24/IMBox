import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import { App } from '@/App';
import '@/styles/style.css';
import { AuthContextProvider } from './contexts/auth-context';

ReactDOM.render(
  <BrowserRouter>
    <AuthContextProvider>
      <App />
    </AuthContextProvider>
  </BrowserRouter>,
  document.getElementById('root'),
);

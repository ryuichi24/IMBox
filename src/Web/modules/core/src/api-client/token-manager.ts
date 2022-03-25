import { Cache } from '../util/cache';

const ACCESS_TOKEN = 'access-token';
const REFRESH_TOKEN = 'refresh-token';

const tokenCache = new Cache();

const accessToken = Object.freeze({
  get: () => tokenCache.get(ACCESS_TOKEN),
  set: (token: string, expiresIn: string) => tokenCache.set(ACCESS_TOKEN, token, expiresIn),
  remove: () => tokenCache.delete(ACCESS_TOKEN),
});

const refreshToken = Object.freeze({
  get: () => localStorage.getItem(REFRESH_TOKEN),
  set: (token: string) => localStorage.setItem(REFRESH_TOKEN, token),
  remove: () => localStorage.removeItem(REFRESH_TOKEN),
});

export const tokenManager = Object.freeze({ accessToken, refreshToken });

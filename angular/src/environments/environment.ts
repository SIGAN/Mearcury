import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'Mearcury',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44389',
    redirectUri: baseUrl,
    clientId: 'Mearcury_App',
    responseType: 'code',
    scope: 'offline_access Mearcury',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44336',
      rootNamespace: 'Mearcury',
    },
  },
} as Environment;

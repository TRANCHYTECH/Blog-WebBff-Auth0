import { ApplicationConfig, importProvidersFrom, provideZoneChangeDetection } from '@angular/core';
import { provideRouter, RouterModule } from '@angular/router';
import { appRoutes } from './app.routes';
import { provideHttpClient, withInterceptorsFromDi, withInterceptors } from '@angular/common/http';
import { AskApiHttpInterceptor } from './intercepters/askapi.httpinterceptor';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { GraphQLModule } from './graphql.module';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(appRoutes),
    provideHttpClient(
      withInterceptorsFromDi(),
      withInterceptors([AskApiHttpInterceptor])
    ),
    importProvidersFrom(CommonModule, BrowserModule, RouterModule, GraphQLModule)
  ],
};
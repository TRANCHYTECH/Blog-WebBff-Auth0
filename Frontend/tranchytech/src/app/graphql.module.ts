import { NgModule } from '@angular/core';
import { ApolloModule, APOLLO_OPTIONS } from 'apollo-angular';
import { InMemoryCache } from '@apollo/client/core';
import { HttpLink } from 'apollo-angular/http';

const uri = 'https://localhost:7042/api/graphql';
export function createApollo(httpLink: HttpLink) {
  return {
    // uri: uri,
    link: httpLink.create({ uri, withCredentials: true }), // use this the result of http intercepter being invoked
    cache: new InMemoryCache(),
    headers: {
      Authorization: 'Bearer <replace this token>',
    },
  };
}

@NgModule({
  exports: [ApolloModule],
  providers: [
    {
      provide: APOLLO_OPTIONS,
      useFactory: createApollo,
      deps: [HttpLink],
    },
  ],
})
export class GraphQLModule {}

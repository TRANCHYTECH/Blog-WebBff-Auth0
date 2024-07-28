import * as Types from '../types';

import { gql } from 'apollo-angular';
import { Injectable } from '@angular/core';
import * as Apollo from 'apollo-angular';
export type QuestionCategoriesQueryVariables = Types.Exact<{ [key: string]: never; }>;


export type QuestionCategoriesQuery = { __typename?: 'Query', questionCategories?: { __typename?: 'QuestionCategoriesConnection', nodes?: Array<{ __typename?: 'QuestionCategory', id: string, key: string }> | null } | null };

export type QuestionsQueryVariables = Types.Exact<{ [key: string]: never; }>;


export type QuestionsQuery = { __typename?: 'Query', questions: Array<{ __typename?: 'Question', id: string }> };

export const QuestionCategoriesDocument = gql`
    query QuestionCategories {
  questionCategories {
    nodes {
      id
      key
    }
  }
}
    `;

  @Injectable({
    providedIn: 'root'
  })
  export class QuestionCategoriesGQL extends Apollo.Query<QuestionCategoriesQuery, QuestionCategoriesQueryVariables> {
    document = QuestionCategoriesDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
export const QuestionsDocument = gql`
    query Questions {
  questions {
    id
  }
}
    `;

  @Injectable({
    providedIn: 'root'
  })
  export class QuestionsGQL extends Apollo.Query<QuestionsQuery, QuestionsQueryVariables> {
    document = QuestionsDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
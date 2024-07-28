import { gql } from "apollo-angular";

export const QuestionCategoriesQuery = gql`
query QuestionCategories {
  questionCategories {
    nodes {
      id
      key
    }
  }
}
`


export const QuestionsQuery = gql`
query Questions {
  questions {
    id
  }
}
`


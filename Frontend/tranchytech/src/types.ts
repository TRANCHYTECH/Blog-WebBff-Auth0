export type Maybe<T> = T | null;
export type InputMaybe<T> = Maybe<T>;
export type Exact<T extends { [key: string]: unknown }> = { [K in keyof T]: T[K] };
export type MakeOptional<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]?: Maybe<T[SubKey]> };
export type MakeMaybe<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]: Maybe<T[SubKey]> };
export type MakeEmpty<T extends { [key: string]: unknown }, K extends keyof T> = { [_ in K]?: never };
export type Incremental<T> = T | { [P in keyof T]?: P extends ' $fragmentName' | '__typename' ? T[P] : never };
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: { input: string; output: string; }
  String: { input: string; output: string; }
  Boolean: { input: boolean; output: boolean; }
  Int: { input: number; output: number; }
  Float: { input: number; output: number; }
  /** The `DateTime` scalar represents an ISO-8601 compliant date time type. */
  DateTime: { input: any; output: any; }
};

export enum ApplyPolicy {
  AfterResolver = 'AFTER_RESOLVER',
  BeforeResolver = 'BEFORE_RESOLVER',
  Validation = 'VALIDATION'
}

export type BooleanOperationFilterInput = {
  eq?: InputMaybe<Scalars['Boolean']['input']>;
  neq?: InputMaybe<Scalars['Boolean']['input']>;
};

export type CreateDepositInput = {
  amount: Scalars['Float']['input'];
  questionId: Scalars['String']['input'];
};

export type CreateDepositPayload = {
  __typename?: 'CreateDepositPayload';
  deposit?: Maybe<Deposit>;
};

export type CreateQuestionError = NotFoundCategoryError;

export type CreateQuestionInput = {
  categoryKeys: Array<Scalars['String']['input']>;
  communityShareAgreement?: InputMaybe<Scalars['Boolean']['input']>;
  priorityKey?: InputMaybe<Scalars['String']['input']>;
  supportLevel: SupportLevel;
  title: Scalars['String']['input'];
};

export type CreateQuestionPayload = {
  __typename?: 'CreateQuestionPayload';
  errors?: Maybe<Array<CreateQuestionError>>;
  question?: Maybe<Question>;
};

export type DateTimeOperationFilterInput = {
  eq?: InputMaybe<Scalars['DateTime']['input']>;
  gt?: InputMaybe<Scalars['DateTime']['input']>;
  gte?: InputMaybe<Scalars['DateTime']['input']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['DateTime']['input']>>>;
  lt?: InputMaybe<Scalars['DateTime']['input']>;
  lte?: InputMaybe<Scalars['DateTime']['input']>;
  neq?: InputMaybe<Scalars['DateTime']['input']>;
  ngt?: InputMaybe<Scalars['DateTime']['input']>;
  ngte?: InputMaybe<Scalars['DateTime']['input']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['DateTime']['input']>>>;
  nlt?: InputMaybe<Scalars['DateTime']['input']>;
  nlte?: InputMaybe<Scalars['DateTime']['input']>;
};

export type Deposit = {
  __typename?: 'Deposit';
  amount: Scalars['Float']['output'];
  id: Scalars['Int']['output'];
  questionId: Scalars['String']['output'];
  status: Scalars['String']['output'];
};

export type DepositFilterInput = {
  amount?: InputMaybe<FloatOperationFilterInput>;
  and?: InputMaybe<Array<DepositFilterInput>>;
  id?: InputMaybe<IntOperationFilterInput>;
  or?: InputMaybe<Array<DepositFilterInput>>;
  questionId?: InputMaybe<StringOperationFilterInput>;
  status?: InputMaybe<StringOperationFilterInput>;
};

export type DepositSortInput = {
  amount?: InputMaybe<SortEnumType>;
  id?: InputMaybe<SortEnumType>;
  questionId?: InputMaybe<SortEnumType>;
  status?: InputMaybe<SortEnumType>;
};

/** A connection to a list of items. */
export type DepositsConnection = {
  __typename?: 'DepositsConnection';
  /** A list of edges. */
  edges?: Maybe<Array<DepositsEdge>>;
  /** A flattened list of the nodes. */
  nodes?: Maybe<Array<Deposit>>;
  /** Information to aid in pagination. */
  pageInfo: PageInfo;
};

/** An edge in a connection. */
export type DepositsEdge = {
  __typename?: 'DepositsEdge';
  /** A cursor for use in pagination. */
  cursor: Scalars['String']['output'];
  /** The item at the end of the edge. */
  node: Deposit;
};

export type Error = {
  message: Scalars['String']['output'];
};

export type FloatOperationFilterInput = {
  eq?: InputMaybe<Scalars['Float']['input']>;
  gt?: InputMaybe<Scalars['Float']['input']>;
  gte?: InputMaybe<Scalars['Float']['input']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['Float']['input']>>>;
  lt?: InputMaybe<Scalars['Float']['input']>;
  lte?: InputMaybe<Scalars['Float']['input']>;
  neq?: InputMaybe<Scalars['Float']['input']>;
  ngt?: InputMaybe<Scalars['Float']['input']>;
  ngte?: InputMaybe<Scalars['Float']['input']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['Float']['input']>>>;
  nlt?: InputMaybe<Scalars['Float']['input']>;
  nlte?: InputMaybe<Scalars['Float']['input']>;
};

export type IntOperationFilterInput = {
  eq?: InputMaybe<Scalars['Int']['input']>;
  gt?: InputMaybe<Scalars['Int']['input']>;
  gte?: InputMaybe<Scalars['Int']['input']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['Int']['input']>>>;
  lt?: InputMaybe<Scalars['Int']['input']>;
  lte?: InputMaybe<Scalars['Int']['input']>;
  neq?: InputMaybe<Scalars['Int']['input']>;
  ngt?: InputMaybe<Scalars['Int']['input']>;
  ngte?: InputMaybe<Scalars['Int']['input']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['Int']['input']>>>;
  nlt?: InputMaybe<Scalars['Int']['input']>;
  nlte?: InputMaybe<Scalars['Int']['input']>;
};

export type KeyValuePairOfStringAndString = {
  __typename?: 'KeyValuePairOfStringAndString';
  key: Scalars['String']['output'];
  value: Scalars['String']['output'];
};

export type KeyValuePairOfStringAndStringFilterInput = {
  and?: InputMaybe<Array<KeyValuePairOfStringAndStringFilterInput>>;
  key?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<KeyValuePairOfStringAndStringFilterInput>>;
  value?: InputMaybe<StringOperationFilterInput>;
};

export type ListFilterInputTypeOfKeyValuePairOfStringAndStringFilterInput = {
  all?: InputMaybe<KeyValuePairOfStringAndStringFilterInput>;
  any?: InputMaybe<Scalars['Boolean']['input']>;
  none?: InputMaybe<KeyValuePairOfStringAndStringFilterInput>;
  some?: InputMaybe<KeyValuePairOfStringAndStringFilterInput>;
};

export type ListQuestionActionOperationFilterInput = {
  all?: InputMaybe<QuestionActionOperationFilterInput>;
  any?: InputMaybe<Scalars['Boolean']['input']>;
  none?: InputMaybe<QuestionActionOperationFilterInput>;
  some?: InputMaybe<QuestionActionOperationFilterInput>;
};

export type ListStringOperationFilterInput = {
  all?: InputMaybe<StringOperationFilterInput>;
  any?: InputMaybe<Scalars['Boolean']['input']>;
  none?: InputMaybe<StringOperationFilterInput>;
  some?: InputMaybe<StringOperationFilterInput>;
};

export type Mutation = {
  __typename?: 'Mutation';
  createDeposit: CreateDepositPayload;
  seedCoreData: SeedCoreDataPayload;
};


export type MutationCreateDepositArgs = {
  input: CreateDepositInput;
};

export type NotFoundCategoryError = Error & {
  __typename?: 'NotFoundCategoryError';
  categoryKeys: Array<Scalars['String']['output']>;
  message: Scalars['String']['output'];
};

/** Information about pagination in a connection. */
export type PageInfo = {
  __typename?: 'PageInfo';
  /** When paginating forwards, the cursor to continue. */
  endCursor?: Maybe<Scalars['String']['output']>;
  /** Indicates whether more edges exist following the set defined by the clients arguments. */
  hasNextPage: Scalars['Boolean']['output'];
  /** Indicates whether more edges exist prior the set defined by the clients arguments. */
  hasPreviousPage: Scalars['Boolean']['output'];
  /** When paginating backwards, the cursor to continue. */
  startCursor?: Maybe<Scalars['String']['output']>;
};

export type Query = {
  __typename?: 'Query';
  deposit?: Maybe<Deposit>;
  deposits?: Maybe<DepositsConnection>;
  questionCategories?: Maybe<QuestionCategoriesConnection>;
  questions: Array<Question>;
};


export type QueryDepositArgs = {
  questionId: Scalars['String']['input'];
};


export type QueryDepositsArgs = {
  after?: InputMaybe<Scalars['String']['input']>;
  before?: InputMaybe<Scalars['String']['input']>;
  first?: InputMaybe<Scalars['Int']['input']>;
  last?: InputMaybe<Scalars['Int']['input']>;
  order?: InputMaybe<Array<DepositSortInput>>;
  where?: InputMaybe<DepositFilterInput>;
};


export type QueryQuestionCategoriesArgs = {
  after?: InputMaybe<Scalars['String']['input']>;
  before?: InputMaybe<Scalars['String']['input']>;
  first?: InputMaybe<Scalars['Int']['input']>;
  last?: InputMaybe<Scalars['Int']['input']>;
  order?: InputMaybe<Array<QuestionCategorySortInput>>;
  where?: InputMaybe<QuestionCategoryFilterInput>;
};


export type QueryQuestionsArgs = {
  order?: InputMaybe<Array<QuestionSortInput>>;
  where?: InputMaybe<QuestionFilterInput>;
};

export type Question = {
  __typename?: 'Question';
  categoryKeys: Array<Scalars['String']['output']>;
  comment: Scalars['String']['output'];
  communityShareAgreement?: Maybe<Scalars['Boolean']['output']>;
  consultant?: Maybe<QuestionConsultant>;
  createdBy: Scalars['String']['output'];
  createdOn: Scalars['DateTime']['output'];
  hasDefaultID: Scalars['Boolean']['output'];
  id: Scalars['String']['output'];
  modifiedOn: Scalars['DateTime']['output'];
  permissions?: Maybe<QuestionPermissions>;
  priorityKey?: Maybe<Scalars['String']['output']>;
  questionCategories: Array<QuestionCategory>;
  status: QuestionStatus;
  supportLevel: SupportLevel;
  title: Scalars['String']['output'];
};

export enum QuestionAction {
  GoToConversation = 'GO_TO_CONVERSATION',
  TakeConsultation = 'TAKE_CONSULTATION'
}

export type QuestionActionOperationFilterInput = {
  eq?: InputMaybe<QuestionAction>;
  in?: InputMaybe<Array<QuestionAction>>;
  neq?: InputMaybe<QuestionAction>;
  nin?: InputMaybe<Array<QuestionAction>>;
};

/** A connection to a list of items. */
export type QuestionCategoriesConnection = {
  __typename?: 'QuestionCategoriesConnection';
  /** A list of edges. */
  edges?: Maybe<Array<QuestionCategoriesEdge>>;
  /** A flattened list of the nodes. */
  nodes?: Maybe<Array<QuestionCategory>>;
  /** Information to aid in pagination. */
  pageInfo: PageInfo;
};

/** An edge in a connection. */
export type QuestionCategoriesEdge = {
  __typename?: 'QuestionCategoriesEdge';
  /** A cursor for use in pagination. */
  cursor: Scalars['String']['output'];
  /** The item at the end of the edge. */
  node: QuestionCategory;
};

export type QuestionCategory = {
  __typename?: 'QuestionCategory';
  createdOn: Scalars['DateTime']['output'];
  id: Scalars['String']['output'];
  key: Scalars['String']['output'];
  modifiedOn: Scalars['DateTime']['output'];
  title: Array<KeyValuePairOfStringAndString>;
};

export type QuestionCategoryFilterInput = {
  and?: InputMaybe<Array<QuestionCategoryFilterInput>>;
  createdOn?: InputMaybe<DateTimeOperationFilterInput>;
  id?: InputMaybe<StringOperationFilterInput>;
  key?: InputMaybe<StringOperationFilterInput>;
  modifiedOn?: InputMaybe<DateTimeOperationFilterInput>;
  or?: InputMaybe<Array<QuestionCategoryFilterInput>>;
  title?: InputMaybe<ListFilterInputTypeOfKeyValuePairOfStringAndStringFilterInput>;
};

export type QuestionCategorySortInput = {
  createdOn?: InputMaybe<SortEnumType>;
  id?: InputMaybe<SortEnumType>;
  key?: InputMaybe<SortEnumType>;
  modifiedOn?: InputMaybe<SortEnumType>;
};

export type QuestionConsultant = {
  __typename?: 'QuestionConsultant';
  attachmentIds: Array<Scalars['String']['output']>;
  conclusion?: Maybe<Scalars['String']['output']>;
  createdAt: Scalars['DateTime']['output'];
  userId: Scalars['String']['output'];
};

export type QuestionConsultantFilterInput = {
  and?: InputMaybe<Array<QuestionConsultantFilterInput>>;
  attachmentIds?: InputMaybe<ListStringOperationFilterInput>;
  conclusion?: InputMaybe<StringOperationFilterInput>;
  createdAt?: InputMaybe<DateTimeOperationFilterInput>;
  or?: InputMaybe<Array<QuestionConsultantFilterInput>>;
  userId?: InputMaybe<StringOperationFilterInput>;
};

export type QuestionConsultantSortInput = {
  conclusion?: InputMaybe<SortEnumType>;
  createdAt?: InputMaybe<SortEnumType>;
  userId?: InputMaybe<SortEnumType>;
};

export type QuestionFilterInput = {
  and?: InputMaybe<Array<QuestionFilterInput>>;
  categoryKeys?: InputMaybe<ListStringOperationFilterInput>;
  comment?: InputMaybe<StringOperationFilterInput>;
  communityShareAgreement?: InputMaybe<BooleanOperationFilterInput>;
  consultant?: InputMaybe<QuestionConsultantFilterInput>;
  createdBy?: InputMaybe<StringOperationFilterInput>;
  createdOn?: InputMaybe<DateTimeOperationFilterInput>;
  id?: InputMaybe<StringOperationFilterInput>;
  modifiedOn?: InputMaybe<DateTimeOperationFilterInput>;
  or?: InputMaybe<Array<QuestionFilterInput>>;
  permissions?: InputMaybe<QuestionPermissionsFilterInput>;
  priorityKey?: InputMaybe<StringOperationFilterInput>;
  status?: InputMaybe<QuestionStatusOperationFilterInput>;
  supportLevel?: InputMaybe<SupportLevelOperationFilterInput>;
  title?: InputMaybe<StringOperationFilterInput>;
};

export type QuestionPermissions = {
  __typename?: 'QuestionPermissions';
  actions: Array<QuestionAction>;
  directChatTargetUserId?: Maybe<Scalars['String']['output']>;
};

export type QuestionPermissionsFilterInput = {
  actions?: InputMaybe<ListQuestionActionOperationFilterInput>;
  and?: InputMaybe<Array<QuestionPermissionsFilterInput>>;
  directChatTargetUserId?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<QuestionPermissionsFilterInput>>;
};

export type QuestionPermissionsSortInput = {
  directChatTargetUserId?: InputMaybe<SortEnumType>;
};

export type QuestionSortInput = {
  comment?: InputMaybe<SortEnumType>;
  communityShareAgreement?: InputMaybe<SortEnumType>;
  consultant?: InputMaybe<QuestionConsultantSortInput>;
  createdBy?: InputMaybe<SortEnumType>;
  createdOn?: InputMaybe<SortEnumType>;
  id?: InputMaybe<SortEnumType>;
  modifiedOn?: InputMaybe<SortEnumType>;
  permissions?: InputMaybe<QuestionPermissionsSortInput>;
  priorityKey?: InputMaybe<SortEnumType>;
  status?: InputMaybe<SortEnumType>;
  supportLevel?: InputMaybe<SortEnumType>;
  title?: InputMaybe<SortEnumType>;
};

export enum QuestionStatus {
  Accepted = 'ACCEPTED',
  BeingReviewed = 'BEING_REVIEWED',
  Cancelled = 'CANCELLED',
  Closed = 'CLOSED',
  InProgress = 'IN_PROGRESS',
  New = 'NEW',
  Payment = 'PAYMENT',
  Rejected = 'REJECTED',
  Reported = 'REPORTED',
  Resolved = 'RESOLVED'
}

export type QuestionStatusOperationFilterInput = {
  eq?: InputMaybe<QuestionStatus>;
  in?: InputMaybe<Array<QuestionStatus>>;
  neq?: InputMaybe<QuestionStatus>;
  nin?: InputMaybe<Array<QuestionStatus>>;
};

export type SeedCoreDataPayload = {
  __typename?: 'SeedCoreDataPayload';
  boolean?: Maybe<Scalars['Boolean']['output']>;
};

export enum SortEnumType {
  Asc = 'ASC',
  Desc = 'DESC'
}

export type StringOperationFilterInput = {
  and?: InputMaybe<Array<StringOperationFilterInput>>;
  contains?: InputMaybe<Scalars['String']['input']>;
  endsWith?: InputMaybe<Scalars['String']['input']>;
  eq?: InputMaybe<Scalars['String']['input']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['String']['input']>>>;
  ncontains?: InputMaybe<Scalars['String']['input']>;
  nendsWith?: InputMaybe<Scalars['String']['input']>;
  neq?: InputMaybe<Scalars['String']['input']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['String']['input']>>>;
  nstartsWith?: InputMaybe<Scalars['String']['input']>;
  or?: InputMaybe<Array<StringOperationFilterInput>>;
  startsWith?: InputMaybe<Scalars['String']['input']>;
};

export type Subscription = {
  __typename?: 'Subscription';
  questionCreated: Question;
};

export enum SupportLevel {
  Agency = 'AGENCY',
  Community = 'COMMUNITY',
  Expert = 'EXPERT'
}

export type SupportLevelOperationFilterInput = {
  eq?: InputMaybe<SupportLevel>;
  in?: InputMaybe<Array<SupportLevel>>;
  neq?: InputMaybe<SupportLevel>;
  nin?: InputMaybe<Array<SupportLevel>>;
};

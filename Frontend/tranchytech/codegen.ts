import type { CodegenConfig } from '@graphql-codegen/cli';

const config: CodegenConfig = {
  overwrite: true,
  schema: '../../Backend/Tranchy.WebBff/schema.graphql',
  documents: 'src/**/!(*.generated).ts',
  generates: {
    'src/types.ts': {
      plugins: ['typescript'],
    },
    'src/': {
      preset: 'near-operation-file',
      presetConfig: {
        extension: '.generated.ts',
        baseTypesPath: 'types.ts',
      },
      plugins: ['typescript-operations', 'typescript-apollo-angular'],
    },
    './graphql.schema.json': {
      plugins: ['introspection'],
    },
  },
};

export default config;

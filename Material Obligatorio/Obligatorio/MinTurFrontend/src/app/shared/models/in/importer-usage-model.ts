import { ImporterParameterIntentModel } from './importer-parameter-intent-model';

export interface ImporterUsageModel {
  importerName: string;
  parameters: ImporterParameterIntentModel[];
}

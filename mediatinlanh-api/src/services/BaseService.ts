import { Model, UpdateQuery } from 'mongoose';

export interface QueryOptions {
  page?: number;
  perPage?: number;
  sort?: Record<string, 1 | -1>;
  populate?: string | string[];
}

export interface PaginatedResult<T> {
  data: T[];
  pagination: {
    total: number;
    page: number;
    perPage: number;
    totalPages: number;
  };
}

// eslint-disable-next-line @typescript-eslint/no-explicit-any
type FilterType = Record<string, any>;

export class BaseService<T> {
  protected model: Model<T>;

  constructor(model: Model<T>) {
    this.model = model;
  }

  async findAll(
    filter: FilterType = {},
    options: QueryOptions = {}
  ): Promise<PaginatedResult<T>> {
    const { page = 1, perPage = 50, sort = {}, populate } = options;
    const skip = (page - 1) * perPage;

    let query = this.model.find(filter).sort(sort).skip(skip).limit(perPage);

    if (populate) {
      if (Array.isArray(populate)) {
        populate.forEach((p) => {
          query = query.populate(p);
        });
      } else {
        query = query.populate(populate);
      }
    }

    const [data, total] = await Promise.all([
      query.lean().exec() as Promise<T[]>,
      this.model.countDocuments(filter),
    ]);

    return {
      data,
      pagination: {
        total,
        page,
        perPage,
        totalPages: Math.ceil(total / perPage),
      },
    };
  }

  async findById(id: string, populate?: string | string[]): Promise<T | null> {
    let query = this.model.findById(id);
    
    if (populate) {
      if (Array.isArray(populate)) {
        populate.forEach((p) => {
          query = query.populate(p);
        });
      } else {
        query = query.populate(populate);
      }
    }

    return query.lean().exec() as Promise<T | null>;
  }

  async findOne(filter: FilterType): Promise<T | null> {
    return this.model.findOne(filter).lean().exec() as Promise<T | null>;
  }

  async create(data: Partial<T>): Promise<T> {
    const document = new this.model(data);
    const saved = await document.save();
    return saved.toObject() as T;
  }

  async update(id: string, data: UpdateQuery<T>): Promise<T | null> {
    return this.model.findByIdAndUpdate(id, data, { new: true }).lean().exec() as Promise<T | null>;
  }

  async delete(id: string): Promise<boolean> {
    const result = await this.model.findByIdAndDelete(id).exec();
    return result !== null;
  }

  async count(filter: FilterType = {}): Promise<number> {
    return this.model.countDocuments(filter);
  }
}

export default BaseService;

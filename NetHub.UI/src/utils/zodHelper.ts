import {ZodError} from "zod";

export function formatZodErrors(error: ZodError) {
  return error.errors.map(e => {
    return {field: e.path.toString(), message: e.message}
  })
}
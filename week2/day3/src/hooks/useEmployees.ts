import { useQuery } from '@tanstack/react-query'
import axiosClient from '../api/axiosClient'
import type { Employee } from '../types/EmployeeTypes'

const fixEmployee = (employee: Employee): Employee => {
  return {
    ...employee,
    active: employee.active === true,
  }
}

export const useEmployees = () => {
  return useQuery({
    queryKey: ['employees'],
    queryFn: async () => {
      const response = await axiosClient.get<Employee[]>('/employees')

      if (!Array.isArray(response.data)) {
        return []
      }

      return response.data.map(fixEmployee)
    },
    staleTime: 1000 * 10,
  })
}

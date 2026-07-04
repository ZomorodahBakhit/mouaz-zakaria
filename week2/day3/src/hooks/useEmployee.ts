import { useQuery } from '@tanstack/react-query'
import axiosClient from '../api/axiosClient'
import type { Employee } from '../types/EmployeeTypes'

const fixEmployee = (employee: Employee): Employee => {
  return {
    ...employee,
    active: employee.active === true,
  }
}

export const useEmployee = (id?: string) => {
  return useQuery({
    queryKey: ['employee', id],
    queryFn: async () => {
      const response = await axiosClient.get<Employee>(`/employees/${id}`)
      return fixEmployee(response.data)
    },
    enabled: Boolean(id),
    staleTime: 1000 * 10,
  })
}

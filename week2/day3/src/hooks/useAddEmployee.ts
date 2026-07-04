import { useMutation, useQueryClient } from '@tanstack/react-query'
import axiosClient from '../api/axiosClient'
import type { Employee } from '../types/EmployeeTypes'

export const useAddEmployee = () => {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: async (employee: Employee) => {
      const response = await axiosClient.post<Employee>('/employees', {
        ...employee,
        active: employee.active === true,
      })
      return response.data
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['employees'] })
    },
  })
}

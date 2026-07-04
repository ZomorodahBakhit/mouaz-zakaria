import { useMutation, useQueryClient } from '@tanstack/react-query'
import axiosClient from '../api/axiosClient'
import type { Employee } from '../types/EmployeeTypes'

export const useUpdateEmployee = () => {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: async ({ id, employee }: { id: string; employee: Employee }) => {
      const response = await axiosClient.put<Employee>(`/employees/${id}`, {
        ...employee,
        active: employee.active === true,
      })
      return response.data
    },
    onSuccess: (_updatedEmployee, variables) => {
      queryClient.invalidateQueries({ queryKey: ['employees'] })
      queryClient.invalidateQueries({ queryKey: ['employee', variables.id] })
    },
  })
}

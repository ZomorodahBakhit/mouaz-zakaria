import { useMutation, useQueryClient } from '@tanstack/react-query'
import axiosClient from '../api/axiosClient'

export const useDeleteEmployee = () => {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: async (id: string) => {
      await axiosClient.delete(`/employees/${id}`)
      return id
    },
    onSuccess: (deletedEmployeeId) => {
      queryClient.invalidateQueries({ queryKey: ['employees'] })
      queryClient.removeQueries({
        queryKey: ['employee', deletedEmployeeId],
      })
    },
  })
}

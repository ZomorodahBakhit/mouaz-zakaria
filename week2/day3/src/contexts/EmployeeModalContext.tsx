import { createContext, useState } from 'react'
import type { ReactNode } from 'react'
import type { Employee } from '../types/EmployeeTypes'

type ModalMode = 'create' | 'edit'

type EmployeeModalContextValue = {
  isEmployeeModalOpen: boolean
  selectedEmployee: Employee | null
  modalMode: ModalMode
  openCreateEmployeeModal: () => void
  openEditEmployeeModal: (employee: Employee) => void
  closeEmployeeModal: () => void
}

const EmployeeModalContext = createContext<EmployeeModalContextValue>({
  isEmployeeModalOpen: false,
  selectedEmployee: null,
  modalMode: 'create',
  openCreateEmployeeModal: () => {},
  openEditEmployeeModal: () => {},
  closeEmployeeModal: () => {},
})

export const EmployeeModalContextProvider = ({ children }: { children: ReactNode }) => {
  const [isEmployeeModalOpen, setEmployeeModalOpen] = useState(false)
  const [selectedEmployee, setSelectedEmployee] = useState<Employee | null>(null)
  const [modalMode, setModalMode] = useState<ModalMode>('create')

  const openCreateEmployeeModal = () => {
    setSelectedEmployee(null)
    setModalMode('create')
    setEmployeeModalOpen(true)
  }

  const openEditEmployeeModal = (employee: Employee) => {
    setSelectedEmployee(employee)
    setModalMode('edit')
    setEmployeeModalOpen(true)
  }

  const closeEmployeeModal = () => {
    setEmployeeModalOpen(false)
    setSelectedEmployee(null)
    setModalMode('create')
  }

  return (
    <EmployeeModalContext.Provider
      value={{
        isEmployeeModalOpen,
        selectedEmployee,
        modalMode,
        openCreateEmployeeModal,
        openEditEmployeeModal,
        closeEmployeeModal,
      }}
    >
      {children}
    </EmployeeModalContext.Provider>
  )
}

export default EmployeeModalContext
